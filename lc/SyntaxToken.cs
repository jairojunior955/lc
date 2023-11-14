namespace lc
{
    internal class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, Object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }


        public override SyntaxKind Kind { get; }

        public int Position { get; }
        public string Text { get; }
        public object Value { get; }
    }
}
