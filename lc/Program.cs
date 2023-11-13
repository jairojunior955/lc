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
                var linha = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(linha))
                    return;

                var  lexer = new Lexer(linha);
                while(true)
                {
                    var token = lexer.ProximoToken();
                    if (token.Tipo == SintaxeTipo.EndOfFileToken)
                        break;

                    Console.Write($"{token.Tipo}: '{token.Texto}'");
                    if (token.Valor != null)
                        Console.Write($" {token.Valor}");

                    Console.WriteLine();
                }
            }
        }
    }

    enum SintaxeTipo
    {
        NumeroToken,
        EspacoToken,
        AdicaoToken,
        SubtracaoToken,
        MultiplicacaoToken,
        DivisaoToken,
        AbreParentesisToken,
        FechaParentesesToken,
        SeparadorToken,
        BadToken,
        EndOfFileToken,
        ExpressaoNumero,
        ExpressaoBinaria
    }

    abstract class SintaxeExpressão  : SyntaxNode
    {
    }
}