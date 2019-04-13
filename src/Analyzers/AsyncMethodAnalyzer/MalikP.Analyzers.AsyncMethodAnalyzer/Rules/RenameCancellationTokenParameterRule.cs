using Microsoft.CodeAnalysis;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Rules
{
    internal static class RenameCancellationTokenParameterRule
    {
        private const string Category = "Naming";
        private static readonly LocalizableString RenameCancellationTokenParameter_Title = new LocalizableResourceString(nameof(Resources.RenameCancellationTokenParameter_Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString RenameCancellationTokenParameter_MessageFormat = new LocalizableResourceString(nameof(Resources.RenameCancellationTokenParameter_MessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString RenameCancellationTokenParameter_Description = new LocalizableResourceString(nameof(Resources.RenameCancellationTokenParameter_Description), Resources.ResourceManager, typeof(Resources));

        public const string RenameCancellationTokenParameterDiagnosticId = "AsyncAnalyzer_RenameCancellationToken";
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            RenameCancellationTokenParameterDiagnosticId,
            RenameCancellationTokenParameter_Title,
            RenameCancellationTokenParameter_MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: RenameCancellationTokenParameter_Description);
    }
}
