// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: MissingCancellationTokenInTaskMethod.cs 
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
using System.Linq;

using MalikP.Analyzers.AsyncMethodAnalyzer.Analyzers;
using MalikP.Analyzers.AsyncMethodAnalyzer.Rules;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MalikP.Analyzers.AsyncMethodAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MissingCancellationTokenInTaskMethod : AbstractDiagnosticAnalyzer
    {
        protected override DiagnosticDescriptor DiagnosticDescriptor => AddMissingCancellationTokenRule.Rule;

        protected override SymbolKind[] SymbolKinds => new[] { SymbolKind.Method };

        protected override void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            IMethodSymbol methodSymbol = (IMethodSymbol)context.Symbol;
            if (methodSymbol == null)
            {
                return;
            }

            if (methodSymbol.MethodKind == MethodKind.PropertyGet
                || methodSymbol.MethodKind == MethodKind.Constructor)
            {
                return;
            }

            INamedTypeSymbol returnTypeSymbol = methodSymbol.ReturnType as INamedTypeSymbol;
            INamedTypeSymbol taskType = context.Compilation.GetTypeByMetadataName(_taskType);
            INamedTypeSymbol voidType = context.Compilation.GetSpecialType(SpecialType.System_Void);
#if (NETSTANDARD1_6)
            if (!Equals(returnTypeSymbol, voidType)
                && (methodSymbol.IsAsync
                    || Equals(returnTypeSymbol, taskType)
                    || returnTypeSymbol.ToString().StartsWith(_taskType)))
#else
            if (!Equals(returnTypeSymbol, voidType)
                && (methodSymbol.IsAsync
                    || Equals(returnTypeSymbol, taskType)
                    || returnTypeSymbol.ToString().StartsWith(_taskType, StringComparison.InvariantCulture)))
#endif
            {
                INamedTypeSymbol cancellationToken = context.Compilation.GetTypeByMetadataName(_cancellationTokenType);
                IParameterSymbol cancellationTokenParameter = methodSymbol.Parameters.FirstOrDefault(d => d.Type == cancellationToken);

                if (cancellationTokenParameter == null)
                {
                    ReportDiagnosticResult(context, methodSymbol);
                }
            }
        }
    }
}