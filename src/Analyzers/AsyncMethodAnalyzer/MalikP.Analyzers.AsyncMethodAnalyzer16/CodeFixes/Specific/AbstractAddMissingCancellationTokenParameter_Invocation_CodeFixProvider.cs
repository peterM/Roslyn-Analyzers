// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: AddMissingCancellationTokenParameterAndUseScoped_Invocation_CodeFixProvider.cs 
// Company: MalikP.
//
// Repository: https://github.com/peterM/Roslyn-Analyzers
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MalikP.Analyzers.AsyncMethodAnalyzer.Models;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Simplification;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.CodeFixes.Specific
{
    public abstract class AbstractAddMissingCancellationTokenParameter_Invocation_CodeFixProvider : AbstractSolutionCodefixProvider<InvocationExpressionSyntax>
    {
        private const string _identifierName = "cancellationToken";

        protected override async Task<Solution> ChangedSolutionHandlerAsync(Document document, InvocationExpressionSyntax syntaxDeclaration, CancellationToken cancellationToken)
        {
            SyntaxNodeReplacementPair invocation = await ConstructInvocationPairAsync(document, syntaxDeclaration, cancellationToken)
                .ConfigureAwait(false);

            SyntaxNodeReplacementPair declaration = await ConstructDeclarationPairAsync(document, syntaxDeclaration, cancellationToken)
                .ConfigureAwait(false);

            Solution solution = document.Project.Solution;
            return await UpdateDocumentsAsync(solution, invocation, declaration, cancellationToken).ConfigureAwait(false);
        }

        protected abstract Task<SyntaxNodeReplacementPair> ConstructInvocationPairAsync(Document document, InvocationExpressionSyntax syntaxDeclaration, CancellationToken cancellationToken);

        private async Task<Solution> UpdateDocumentsAsync(Solution solution, SyntaxNodeReplacementPair invocation, SyntaxNodeReplacementPair declaration, CancellationToken cancellationToken)
        {
            if (AreTheSameDocuments(invocation.Document, declaration.Document))
            {
                try
                {
                    SyntaxNode root = invocation.Root.ReplaceNode(invocation.OriginalNode, invocation.ReplacementNode);

                    SyntaxNode found = root.FindNode(declaration.OriginalNode.Span);
                    root = root.ReplaceNode(found, declaration.ReplacementNode);

                    solution = solution.WithDocumentSyntaxRoot(declaration.Document.Id, root);
                }
                catch
                {
                    // TODO: add loging or improve logic to be sure that exception never occure
                }
            }
            else
            {
                solution = await ReplaceAndSimplifyNodeAsync(solution, invocation, cancellationToken)
                    .ConfigureAwait(false);

                solution = await ReplaceAndSimplifyNodeAsync(solution, declaration, cancellationToken)
                    .ConfigureAwait(false);
            }

            return solution;
        }

        private bool AreTheSameDocuments(Document invocationDocument, Document declarationDocument)
        {
            return invocationDocument.Id == declarationDocument.Id;
        }

        private async Task<Solution> ReplaceAndSimplifyNodeAsync(Solution solution, SyntaxNodeReplacementPair pair, CancellationToken cancellationToken)
        {
            SyntaxNode node = pair.Root.ReplaceNode(pair.OriginalNode, pair.ReplacementNode.WithAdditionalAnnotations(Simplifier.Annotation));

            node = await SimplifyAsync(solution, pair.Document.Id, node, cancellationToken).ConfigureAwait(false);

            return solution.WithDocumentSyntaxRoot(pair.Document.Id, node);
        }

        private async Task<SyntaxNode> SimplifyAsync(Solution solution, DocumentId documentId, SyntaxNode root, CancellationToken cancellationToken)
        {
            Document document = solution.GetDocument(documentId);

            document = document.WithSyntaxRoot(root);

            DocumentOptionSet options = await document.GetOptionsAsync(cancellationToken).ConfigureAwait(false);

            document = await Simplifier.ReduceAsync(document, options, cancellationToken).ConfigureAwait(false);

            return await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        }

        private async Task<SyntaxNodeReplacementPair> ConstructDeclarationPairAsync(Document document, InvocationExpressionSyntax syntaxDeclaration, CancellationToken cancellationToken)
        {
            SemanticModel semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
            ISymbol symbol = semanticModel.GetSymbolInfo(syntaxDeclaration).Symbol;

            SyntaxReference declaringSyntax = symbol.DeclaringSyntaxReferences.FirstOrDefault();

            MethodDeclarationSyntax methodSyntax = declaringSyntax.GetSyntax(cancellationToken) as MethodDeclarationSyntax;

            SyntaxToken identifier = SyntaxFactory.Identifier(_identifierName);
            TypeSyntax typeName = SyntaxFactory.ParseTypeName(typeof(CancellationToken).FullName);

            ParameterSyntax parameter = SyntaxFactory
                .Parameter(identifier)
                .WithType(typeName);

            MethodDeclarationSyntax updatedMethod = methodSyntax.AddParameterListParameters(parameter);

            document = document.Project.Solution.GetDocument(declaringSyntax.SyntaxTree);

            return new SyntaxNodeReplacementPair(document, declaringSyntax.SyntaxTree.GetRoot(), methodSyntax, updatedMethod);
        }
    }
}