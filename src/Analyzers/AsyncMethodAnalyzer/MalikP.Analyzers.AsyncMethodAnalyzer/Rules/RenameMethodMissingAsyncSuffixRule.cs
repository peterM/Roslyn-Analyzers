using Microsoft.CodeAnalysis;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Rules
{
    internal static class RenameMethodMissingAsyncSuffixRule
    {
        private const string Category = "Naming";

        private static readonly LocalizableString RenameMethodMissingAsyncSuffix_Title = new LocalizableResourceString(nameof(Resources.RenameMethodMissingAsyncSuffix_Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString RenameMethodMissingAsyncSuffix_MessageFormat = new LocalizableResourceString(nameof(Resources.RenameMethodMissingAsyncSuffix_MessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString RenameMethodMissingAsyncSuffix_Description = new LocalizableResourceString(nameof(Resources.RenameMethodMissingAsyncSuffix_Description), Resources.ResourceManager, typeof(Resources));

        public const string RenameMethodMissingAsyncSuffixDiagnosticId = "AsyncAnalyzer_AddMissingAsyncSuffix";
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            RenameMethodMissingAsyncSuffixDiagnosticId,
            RenameMethodMissingAsyncSuffix_Title,
            RenameMethodMissingAsyncSuffix_MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: RenameMethodMissingAsyncSuffix_Description);
    }
}
