// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: ReorderCancellationTokenParameter.cs 
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

using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MalikP.Analyzers.AsyncMethodAnalyzer.Rules;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.CodeFixes.Specific
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddMissingAsyncSuffixCodeFixProvider)), Shared]
    public sealed class ReorderCancellationTokenParameter : AbstractDocumentCodefixProvider<MethodDeclarationSyntax>
    {
        private const string _identifierType = "CancellationToken";

        protected override string Title => "Reorder 'CancellationToken' as last";

        protected override string DiagnosticId => ReorderCancellationTokenMethodParameterRule.ReorderCancellationTokenMethodParameterDiagnosticId;

        protected override Task<Document> ChangedDocumentHandlerAsync(Document document, MethodDeclarationSyntax syntaxDeclaration, CancellationToken cancellationToken)
        {
            List<ParameterSyntax> parameters = syntaxDeclaration.ParameterList.Parameters.Where(parameterSyntax => !CompareParameter(parameterSyntax)).ToList();

            ParameterSyntax originalCancellationToken = syntaxDeclaration.ParameterList.Parameters.FirstOrDefault(CompareParameter);
            if (originalCancellationToken == null)
            {
                return Task.FromResult(document);
            }

            ParameterListSyntax updatedParameterList = SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(parameters.Concat(new[] { originalCancellationToken })));

            SyntaxNode newNode = ((SyntaxNode)syntaxDeclaration).ReplaceNode(syntaxDeclaration.ParameterList, updatedParameterList);

            CompilationUnitSyntax root = (CompilationUnitSyntax)syntaxDeclaration.SyntaxTree.GetRoot();
            CompilationUnitSyntax newRoot = root.ReplaceNode(syntaxDeclaration.ParameterList, updatedParameterList);

            return Task.FromResult(document.WithSyntaxRoot(newRoot));
        }

        private static bool CompareParameter(ParameterSyntax parameterSyntax)
        {
            return parameterSyntax.Type.ToString().Contains(_identifierType);
        }
    }
}
