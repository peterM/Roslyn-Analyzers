// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: AbstractCodefixProvider.cs 
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
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.CodeFixes
{
    public abstract class AbstractCodefixProvider<TSpecificSyntax> : CodeFixProvider
        where TSpecificSyntax : CSharpSyntaxNode
    {
        protected AbstractCodefixProvider()
        {
        }

        protected abstract string EquivalenceKey { get; }

        protected abstract string Title { get; }

        protected abstract string DiagnosticId { get; }

        protected abstract void RegisterSolutionCodeFix(CodeFixContext context, Diagnostic diagnostic, TSpecificSyntax specificSyntax);

        protected abstract void RegisterDocumentCodeFix(CodeFixContext context, Diagnostic diagnostic, TSpecificSyntax specificSyntax);

        protected abstract Task<Solution> ChangedSolutionHandlerAsync(Document document, TSpecificSyntax syntaxDeclaration, CancellationToken cancellationToken);

        protected abstract Task<Document> ChangedDocumentHandlerAsync(Document document, TSpecificSyntax syntaxDeclaration, CancellationToken cancellationToken);

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiagnosticId);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            RegisterCodeFix(context, root, DiagnosticId);
        }

        private TextSpan GetDiagnosticSpan(Diagnostic diagnostic)
        {
            return diagnostic.Location.SourceSpan;
        }

        private IEnumerable<TSyntax> GetSyntaxes<TSyntax>(SyntaxNode root, TextSpan diagnosticSpan)
            where TSyntax : CSharpSyntaxNode
        {
            return root.FindToken(diagnosticSpan.Start)
                .Parent
                .AncestorsAndSelf()
                .OfType<TSyntax>();
        }

        private void RegisterCodeFix(CodeFixContext context, SyntaxNode root, string diagnosticId)
        {
            Diagnostic diagnostic = GetDiagnostic(context, diagnosticId);
            if (diagnostic == null)
            {
                return;
            }

            TextSpan diagnosticSpan = GetDiagnosticSpan(diagnostic);
            IEnumerable<TSpecificSyntax> syntaxes = GetSyntaxes<TSpecificSyntax>(root, diagnosticSpan);
            TSpecificSyntax specificSyntax = GetSpecificSyntax(syntaxes);

            RegisterDocumentCodeFix(context, diagnostic, specificSyntax);
            RegisterSolutionCodeFix(context, diagnostic, specificSyntax);
        }

        private Diagnostic GetDiagnostic(CodeFixContext context, string diagnosticId)
        {
            return context
                .Diagnostics
                .FirstOrDefault(diagnosticItem => diagnosticItem.Id == diagnosticId);
        }

        protected virtual TSpecificSyntax GetSpecificSyntax(IEnumerable<TSpecificSyntax> syntaxes)
        {
            return syntaxes.First();
        }
    }
}