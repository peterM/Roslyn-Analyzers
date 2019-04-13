// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: AsyncMethodAnalyzerCodeFixProvider.cs 
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

using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MalikP.Analyzers.AsyncMethodAnalyzer.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace MalikP.Analyzers.AsyncMethodAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AsyncMethodAnalyzerCodeFixProvider)), Shared]
    public class AsyncMethodAnalyzerCodeFixProvider : CodeFixProvider
    {
        private const string _renameCancellationTokenParameterTitle = "Rename to 'cancellationToken'";
        private const string _addMissingAsyncSuffixTitle = "Add missing 'Async' suffix";
        private const string _addMissingCancellationTokenParameterTitle = "Add missing 'CancellationToken'";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get
            {
                return ImmutableArray.Create(
                    RenameCancellationTokenParameterRule.RenameCancellationTokenParameterDiagnosticId,
                    RenameMethodMissingAsyncSuffixRule.RenameMethodMissingAsyncSuffixDiagnosticId,
                    AddMissingCancellationTokenRule.AddMissingCancellationTokenDiagnosticId);
            }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            Register_RenameCancellationTokenParameter(context, root);

            Register_AddMissingAsyncSuffix(context, root);

            Register_AddMissingCancellationTokenParameter(context, root);
        }

        private void Register_AddMissingCancellationTokenParameter(CodeFixContext context, SyntaxNode root)
        {
            Diagnostic diagnostic = context
                .Diagnostics
                .FirstOrDefault(riagnosticItem => riagnosticItem.Id == AddMissingCancellationTokenRule.AddMissingCancellationTokenDiagnosticId);
            if (diagnostic == null)
            {
                return;
            }

            TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;
            MethodDeclarationSyntax declaration = root
                .FindToken(diagnosticSpan.Start)
                .Parent
                .AncestorsAndSelf()
                .OfType<MethodDeclarationSyntax>()
                .First();

            context.RegisterCodeFix(
               CodeAction.Create(
                   title: _addMissingCancellationTokenParameterTitle,
                   createChangedDocument: cancellationToken => MissingCancellationToken(context.Document, declaration, cancellationToken),
                   equivalenceKey: _addMissingCancellationTokenParameterTitle),
               diagnostic);
        }

        private void Register_RenameCancellationTokenParameter(CodeFixContext context, SyntaxNode root)
        {
            Diagnostic diagnostic = context
                .Diagnostics
                .FirstOrDefault(diagnosticItem => diagnosticItem.Id == RenameCancellationTokenParameterRule.RenameCancellationTokenParameterDiagnosticId);
            if (diagnostic == null)
            {
                return;
            }

            TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;

            ParameterSyntax declaration = root
                .FindToken(diagnosticSpan.Start)
                .Parent
                .AncestorsAndSelf()
                .OfType<ParameterSyntax>()
                .First(parameterSyntaxItem => parameterSyntaxItem.Type.ToString() == "System.Threading.CancellationToken");

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: _renameCancellationTokenParameterTitle,
                    createChangedSolution: cancellationToken => RenameMethodParameterToCancellationToken(context.Document, declaration, cancellationToken),
                    equivalenceKey: _renameCancellationTokenParameterTitle),
                diagnostic);
        }

        private void Register_AddMissingAsyncSuffix(CodeFixContext context, SyntaxNode root)
        {
            Diagnostic diagnostic = context
                .Diagnostics
                .FirstOrDefault(diagnosticItem => diagnosticItem.Id == RenameMethodMissingAsyncSuffixRule.RenameMethodMissingAsyncSuffixDiagnosticId);
            if (diagnostic == null)
            {
                return;
            }

            TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;
            MethodDeclarationSyntax declaration1 = root
                .FindToken(diagnosticSpan.Start)
                .Parent
                .AncestorsAndSelf()
                .OfType<MethodDeclarationSyntax>()
                .First();

            context.RegisterCodeFix(
               CodeAction.Create(
                   title: _addMissingAsyncSuffixTitle,
                   createChangedSolution: cancellationToken => RenameMethodAddAsyncSuffix(context.Document, declaration1, cancellationToken),
                   equivalenceKey: _addMissingAsyncSuffixTitle),
               diagnostic);
        }

        private async Task<Solution> RenameMethodParameterToCancellationToken(Document document, ParameterSyntax typeDecl, CancellationToken cancellationToken)
        {
            SemanticModel semanticModel = await document.GetSemanticModelAsync(cancellationToken);

            IParameterSymbol typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

            Solution originalSolution = document.Project.Solution;
            OptionSet optionSet = originalSolution.Workspace.Options;

            string newName = "cancellationToken";
            return await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);
        }

        private async Task<Solution> RenameMethodAddAsyncSuffix(Document document, MethodDeclarationSyntax typeDecl, CancellationToken cancellationToken)
        {
            SyntaxToken identifierToken = typeDecl.Identifier;
            string newName = string.Concat(identifierToken.Text, "Async");

            SemanticModel semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            IMethodSymbol typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

            Solution originalSolution = document.Project.Solution;
            OptionSet optionSet = originalSolution.Workspace.Options;

            return await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);
        }

        private Task<Document> MissingCancellationToken(Document document, MethodDeclarationSyntax declaration1, CancellationToken cancellationToken)
        {
            return AddMissingCancellationToken(document, declaration1, cancellationToken);
        }

        private async Task<Document> AddMissingCancellationToken(Document document, MethodDeclarationSyntax method, CancellationToken cancellationToken)
        {
            MethodDeclarationSyntax updatedMethod = method.AddParameterListParameters(
                SyntaxFactory.Parameter(
                    SyntaxFactory.Identifier("cancellationToken"))
                                 .WithType(SyntaxFactory.ParseTypeName(typeof(CancellationToken).FullName)));

            SyntaxTree syntaxTree = await document.GetSyntaxTreeAsync(cancellationToken);

            SyntaxNode updatedSyntaxTree = syntaxTree.GetRoot().ReplaceNode(method, updatedMethod);

            return document.WithSyntaxRoot(updatedSyntaxTree);
        }
    }
}
