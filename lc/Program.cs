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

    abstract class SintaxeNode
    {
        public abstract SintaxeTipo Tipo { get; }
    }

    abstract class SintaxeExpressão  : SintaxeNode
    {

    }

    sealed class SintaxeExpressaoNumero : SintaxeExpressão
    {
        public SintaxeExpressaoNumero(SintaxeToken numeroToken)
        {
            NumeroToken = numeroToken;
        }

        public override SintaxeTipo Tipo => SintaxeTipo.ExpressaoNumero;
        public SintaxeToken NumeroToken { get;  }
    }

    sealed class SintaxeExpressaoBinaria : SintaxeExpressão
    {
        public SintaxeExpressaoBinaria(SintaxeExpressão esquerda, SintaxeToken operadorToken, SintaxeExpressão direita)
        {
            Esquerda = esquerda;
            OperadorToken = operadorToken;
            Direita = direita;
        }

        public override SintaxeTipo Tipo => SintaxeTipo.ExpressaoBinaria;
        public SintaxeExpressão Esquerda { get; }
        public SintaxeToken OperadorToken { get; }
        public SintaxeExpressão Direita { get; }
    }

    class Parser
    {
        private readonly SintaxeToken[] _tokens;
        private int _posicao;

        public Parser(string texto)
        {
            var tokens = new List<SintaxeToken>();


            var lexer = new Lexer(texto);
            SintaxeToken token;
            do
            {
                token = lexer.ProximoToken();

                if (token.Tipo != SintaxeTipo.EspacoToken &&
                    token.Tipo != SintaxeTipo.BadToken)
                {
                    tokens.Add(token);
                }
            } while (token.Tipo != SintaxeTipo.EndOfFileToken);

            _tokens = tokens.ToArray();
        }

        private SintaxeToken Peek(int offset)
        {
            var index = _posicao + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];
            return _tokens[index];
        }

        private SintaxeToken Atual => Peek(0);

        private SintaxeToken ProximoToken()
        {
            var atual = Atual;
            _posicao++;
            return atual;
        }

        private SintaxeToken Combinado(SintaxeTipo tipo)
        {
            if (Atual.Tipo == tipo)
            {
                return ProximoToken();
            }
            return new SintaxeToken(tipo, Atual.Posicao, null, null);
        }

        public SintaxeExpressão Parse()
        {
            var esquerda = ExpressaoParsePrimaria();
            while (Atual.Tipo == SintaxeTipo.AdicaoToken ||
                   Atual.Tipo == SintaxeTipo.SubtracaoToken)
            {
                var operadorToken = ProximoToken();
                var direita = ExpressaoParsePrimaria();
                esquerda = new SintaxeExpressaoBinaria(esquerda, operadorToken, direita);
            }

            return esquerda;
        }

        private SintaxeExpressão ExpressaoParsePrimaria()
        {
            var numeroToken = Combinado(SintaxeTipo.NumeroToken);
            return new SintaxeExpressaoNumero(numeroToken);
        }
    }

}