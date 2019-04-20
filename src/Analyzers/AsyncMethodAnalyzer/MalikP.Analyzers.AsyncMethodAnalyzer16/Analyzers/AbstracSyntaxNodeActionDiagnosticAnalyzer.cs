﻿// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: AbstracSyntaxNodeActionDiagnosticAnalyzer.cs 
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

using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Analyzers
{
    public abstract class AbstracSyntaxNodeActionDiagnosticAnalyzer : AbstractDiagnosticAnalyzer
    {
        protected abstract SyntaxKind[] SyntaxKinds { get; }

        protected abstract void AnalyzeNode(SyntaxNodeAnalysisContext context);

        public override void Initialize(AnalysisContext context) => context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKinds);

        protected void ReportDiagnosticResult(SyntaxNodeAnalysisContext context, SyntaxNode syntaxNode)
        {
            Diagnostic diagnostic = Diagnostic.Create(DiagnosticDescriptor, syntaxNode.GetLocation(), syntaxNode.GetText(Encoding.UTF8).ToString().TrimStart().TrimEnd());
            context.ReportDiagnostic(diagnostic);
        }
    }
}