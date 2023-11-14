namespace lc
{
    // 1 + 2 * 3
    //
    //
    //
    //     +
    //    / \
    //   1   *
    //      / \
    //     2   3

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) { 
                Console.Write("$ ");
                var Line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(Line))
                    return;

                var  lexer = new Lexer(Line);
                while(true)
                {
                    var token = lexer.NextToken();
                    if (token.Kind == SyntaxKind.EndOfFileToken)
                        break;

                    Console.Write($"{token.Kind}: '{token.Text}'");
                    if (token.Value != null)
                        Console.Write($" {token.Value}");

                    Console.WriteLine();
                }
            }
        }
    }

    enum SyntaxKind
    {
        NumberToken,
        WhitespaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        SeparatorToken,
        BadToken,
        EndOfFileToken,
        NumberExpression,
        BinaryExpression
    }

    abstract class ExpressionSyntax : SyntaxNode
    {
    }
}