using Microsoft.CodeAnalysis;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Common
{
    public static class SymbolExtensions
    {
        public static bool IsDeclareInMetadata(this ISymbol symbol)
        {
            return !(symbol is null) && !symbol.DeclaringSyntaxReferences.IsDefaultOrEmpty && !symbol.IsImplicitlyDeclared;
        }
    }
}
