// MIT License
//
// Copyright (c) 2019 Peter Malik.
// 
// File: AsyncMethodAnalyzerAnalyzer.cs 
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
using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MalikP.Analyzers.AsyncMethodAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AsyncMethodAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        private const string Category = "Naming";

        public const string RenameCancellationTokenParameterDiagnosticId = "AsyncAnalyzer_RenameCancellationToken";
        private static readonly LocalizableString RenameCancellationTokenParameter_Title = new LocalizableResourceString(nameof(Resources.RenameCancellationTokenParameter_Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString RenameCancellationTokenParameter_MessageFormat = new LocalizableResourceString(nameof(Resources.RenameCancellationTokenParameter_MessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString RenameCancellationTokenParameter_Description = new LocalizableResourceString(nameof(Resources.RenameCancellationTokenParameter_Description), Resources.ResourceManager, typeof(Resources));
        private static DiagnosticDescriptor RenameCancellationTokenParameterRule = new DiagnosticDescriptor(
            RenameCancellationTokenParameterDiagnosticId,
            RenameCancellationTokenParameter_Title,
            RenameCancellationTokenParameter_MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: RenameCancellationTokenParameter_Description);

        public const string RenameMethodMissingAsyncSuffixDiagnosticId = "AsyncAnalyzer_AddMissingAsyncSuffix";
        private static readonly LocalizableString RenameMethodMissingAsyncSuffix_Title = new LocalizableResourceString(nameof(Resources.RenameMethodMissingAsyncSuffix_Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString RenameMethodMissingAsyncSuffix_MessageFormat = new LocalizableResourceString(nameof(Resources.RenameMethodMissingAsyncSuffix_MessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString RenameMethodMissingAsyncSuffix_Description = new LocalizableResourceString(nameof(Resources.RenameMethodMissingAsyncSuffix_Description), Resources.ResourceManager, typeof(Resources));
        private static DiagnosticDescriptor RenameMethodMissingAsyncSuffixRule = new DiagnosticDescriptor(
            RenameMethodMissingAsyncSuffixDiagnosticId,
            RenameMethodMissingAsyncSuffix_Title,
            RenameMethodMissingAsyncSuffix_MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: RenameMethodMissingAsyncSuffix_Description);

        public const string AddMissingCancellationTokenDiagnosticId = "AsyncAnalyzer_AddMissingCancellationToken";
        private static readonly LocalizableString AddMissingCancellationToken_Title = new LocalizableResourceString(nameof(Resources.AddMissingCancellationToken_Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString AddMissingCancellationToken_MessageFormat = new LocalizableResourceString(nameof(Resources.AddMissingCancellationToken_MessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString AddMissingCancellationToken_Description = new LocalizableResourceString(nameof(Resources.AddMissingCancellationToken_Description), Resources.ResourceManager, typeof(Resources));
        private static DiagnosticDescriptor AddMissingCancellationTokenRule = new DiagnosticDescriptor(
            AddMissingCancellationTokenDiagnosticId,
            AddMissingCancellationToken_Title,
            AddMissingCancellationToken_MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: AddMissingCancellationToken_Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return ImmutableArray.Create(
                    RenameCancellationTokenParameterRule,
                    RenameMethodMissingAsyncSuffixRule,
                    AddMissingCancellationTokenRule);
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            INamedTypeSymbol namedTypeSymbol = (INamedTypeSymbol)context.Symbol;

            if (namedTypeSymbol.TypeKind == TypeKind.Class
                || namedTypeSymbol.TypeKind == TypeKind.Interface
                || namedTypeSymbol.TypeKind == TypeKind.Struct)
            {
                INamedTypeSymbol voidType = context.Compilation.GetSpecialType(SpecialType.System_Void);
                INamedTypeSymbol taskType = context.Compilation.GetTypeByMetadataName("System.Threading.Tasks.Task");

                ImmutableArray<ISymbol> members = namedTypeSymbol.GetMembers();
                foreach (IMethodSymbol methodSymbol in members.OfType<IMethodSymbol>())
                {
                    if (methodSymbol.MethodKind == MethodKind.PropertyGet
                        || methodSymbol.MethodKind == MethodKind.Constructor)
                    {
                        continue;
                    }

                    INamedTypeSymbol returnTypeSymbol = methodSymbol.ReturnType as INamedTypeSymbol;
                    if (!Equals(returnTypeSymbol, voidType)
                        && (methodSymbol.IsAsync
                            || Equals(returnTypeSymbol, taskType)
                            || returnTypeSymbol.ToString().StartsWith("System.Threading.Tasks.Task<", StringComparison.InvariantCulture)))
                    {
                        if (!methodSymbol.Name.EndsWith("Async", StringComparison.InvariantCulture))
                        {
                            // Method does not have Async Suffix

                            Diagnostic diagnostic = Diagnostic.Create(RenameMethodMissingAsyncSuffixRule, methodSymbol.Locations[0], namedTypeSymbol.Name);
                            context.ReportDiagnostic(diagnostic);
                        }

                        INamedTypeSymbol cancellationToken = context.Compilation.GetTypeByMetadataName("System.Threading.CancellationToken");
                        IParameterSymbol cancellationTokenParameter = methodSymbol.Parameters.FirstOrDefault(d => d.Type == cancellationToken);

                        if (cancellationTokenParameter == null)
                        {
                            // Method do not contain Cancellation token

                            Diagnostic diagnostic = Diagnostic.Create(AddMissingCancellationTokenRule, methodSymbol.Locations[0], namedTypeSymbol.Name);
                            context.ReportDiagnostic(diagnostic);
                        }
                        else if (cancellationTokenParameter != null
                            && !string.Equals(cancellationTokenParameter.Name, "cancellationToken", StringComparison.InvariantCulture))
                        {
                            // cancellation token has incorrect name

                            Diagnostic diagnostic = Diagnostic.Create(RenameCancellationTokenParameterRule, cancellationTokenParameter.Locations[0], namedTypeSymbol.Name);
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }
    }
}
