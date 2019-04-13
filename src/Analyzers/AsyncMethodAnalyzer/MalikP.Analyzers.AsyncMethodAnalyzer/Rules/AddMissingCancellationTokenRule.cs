using Microsoft.CodeAnalysis;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Rules
{
    internal static class AddMissingCancellationTokenRule
    {
        private const string Category = "Naming";

        private static readonly LocalizableString AddMissingCancellationToken_Title = new LocalizableResourceString(nameof(Resources.AddMissingCancellationToken_Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString AddMissingCancellationToken_MessageFormat = new LocalizableResourceString(nameof(Resources.AddMissingCancellationToken_MessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString AddMissingCancellationToken_Description = new LocalizableResourceString(nameof(Resources.AddMissingCancellationToken_Description), Resources.ResourceManager, typeof(Resources));

        public const string AddMissingCancellationTokenDiagnosticId = "AsyncAnalyzer_AddMissingCancellationToken";
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            AddMissingCancellationTokenDiagnosticId,
            AddMissingCancellationToken_Title,
            AddMissingCancellationToken_MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: AddMissingCancellationToken_Description);
    }
}
