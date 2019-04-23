using Microsoft.CodeAnalysis;

namespace MalikP.Analyzers.AsyncMethodAnalyzer.Models
{
    public class SyntaxNodeReplacementPair
    {
        public SyntaxNodeReplacementPair(Document document, SyntaxNode root, SyntaxNode originalNode, SyntaxNode replacementNode)
        {
            Document = document;
            OriginalNode = originalNode;
            ReplacementNode = replacementNode;
            Root = root;
        }

        public Document Document { get; }
        public SyntaxNode OriginalNode { get; }
        public SyntaxNode ReplacementNode { get; }
        public SyntaxNode Root { get; }
    }
}
