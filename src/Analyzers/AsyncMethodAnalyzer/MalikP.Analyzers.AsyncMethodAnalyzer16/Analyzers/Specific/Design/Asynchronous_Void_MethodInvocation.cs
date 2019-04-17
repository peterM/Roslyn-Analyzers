using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Analyzers.Specific.Design
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class Asynchronous_Void_MethodInvocation : AbstracSyntaxNodeActionDiagnosticAnalyzer
    {
        protected override SyntaxKind[] SyntaxKinds =>
            new[]
            {
                SyntaxKind.InvocationExpression
            };

        protected override DiagnosticDescriptor DiagnosticDescriptor => null;

        protected override void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
        }
    }
}
