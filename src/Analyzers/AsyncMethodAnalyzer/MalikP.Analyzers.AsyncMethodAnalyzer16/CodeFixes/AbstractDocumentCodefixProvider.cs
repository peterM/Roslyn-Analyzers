// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: AbstractDocumentCodefixProvider.cs 
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

using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.CodeFixes
{
    public abstract class AbstractDocumentCodefixProvider<TSpecificSyntax> : AbstractCodefixProvider<TSpecificSyntax>
       where TSpecificSyntax : CSharpSyntaxNode
    {
        protected AbstractDocumentCodefixProvider()
        {
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void RegisterSolutionCodeFix(CodeFixContext context, Diagnostic diagnostic, TSpecificSyntax specificSyntax)
        {
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void RegisterDocumentCodeFix(CodeFixContext context, Diagnostic diagnostic, TSpecificSyntax specificSyntax)
        {
            context.RegisterCodeFix(
               CodeAction.Create(
                   title: Title,
                   createChangedDocument: cancellationToken => ChangedDocumentHandlerAsync(context.Document, specificSyntax, cancellationToken),
                   equivalenceKey: EquivalenceKey),
               diagnostic);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override Task<Solution> ChangedSolutionHandlerAsync(Document document, TSpecificSyntax syntaxDeclaration, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}