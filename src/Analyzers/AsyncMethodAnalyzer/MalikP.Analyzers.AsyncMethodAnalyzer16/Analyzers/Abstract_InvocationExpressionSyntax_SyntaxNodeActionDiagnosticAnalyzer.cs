// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: Abstract_InvocationExpressionSyntax_SyntaxNodeActionDiagnosticAnalyzer.cs 
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
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Analyzers
{
    public abstract class Abstract_InvocationExpressionSyntax_SyntaxNodeActionDiagnosticAnalyzer : AbstractSyntaxNodeActionDiagnosticAnalyzer
    {
        protected override SyntaxKind[] SyntaxKinds =>
            new[]
            {
                SyntaxKind.InvocationExpression
            };

        protected AnalyzerCanContinueMethodResult GetContinuationResult(SyntaxNodeAnalysisContext context)
        {
            var result = AnalyzerCanContinueMethodResult.Default();
            if (!(context.Node is InvocationExpressionSyntax invocationExpressionSyntax))
            {
                return result;
            }

            if (!(context.SemanticModel.GetSymbolInfo(invocationExpressionSyntax).Symbol is IMethodSymbol methodSymbol))
            {
                return result;
            }

            if (methodSymbol.MethodKind == MethodKind.PropertyGet
               || methodSymbol.MethodKind == MethodKind.Constructor)
            {
                return result;
            }

            SyntaxReference syntaxReference = methodSymbol
                .DeclaringSyntaxReferences
                .FirstOrDefault();

            if (syntaxReference == null)
            {
                return result;
            }

            if (!(methodSymbol?.ReturnType is INamedTypeSymbol returnTypeSymbol))
            {
                return result;
            }

            return new AnalyzerCanContinueMethodResult(methodSymbol, returnTypeSymbol, true);
        }
    }
}
