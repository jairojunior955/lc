namespace lc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("> ");
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
        BadToken,
        EndOfFileToken
    }

    class SintaxeToken
    {
        public SintaxeToken(SintaxeTipo tipo, int posicao, string texto, Object valor) 
        {
            Tipo = tipo;
            Posicao = posicao;
            Texto = texto;
            Valor = valor;
        }

        public SintaxeTipo Tipo { get; }
        public int Posicao { get; }
        public string Texto { get; }
        public object Valor { get; }
    }

    class Lexer
    {
        private readonly string _texto;
        private int _posicao;

        public Lexer(string texto)
        {
            _texto = texto; 
        }

        private char Atual
        {
            get
            {
                if (_posicao >= _texto.Length)
                    return '\0';

                return _texto[_posicao];
            }
        }

        private void Proximo()
        {
            _posicao++;
        }

        public SintaxeToken ProximoToken()
        {
            // <numeros>
            // + - * / ( )
            // <espaço>

            if (_posicao >= _texto.Length)
            {
                return new SintaxeToken(SintaxeTipo.EndOfFileToken, _posicao, "\0", null);
            }

            if (char.IsDigit(Atual))
            {
                var inicio = _posicao;

                while (char.IsDigit(Atual))
                    Proximo();

                var length = _posicao - inicio;
                var texto = _texto.Substring(inicio, length);
                return new SintaxeToken(SintaxeTipo.NumeroToken, inicio, texto, null);
            }

            if (char.IsWhiteSpace(Atual)) 
            {
                var inicio = _posicao;

                while (char.IsWhiteSpace(Atual))
                    Proximo();

                var length = _posicao - inicio;
                var texto = _texto.Substring(inicio, length);
                int.TryParse(texto, out var valor);
                return new SintaxeToken(SintaxeTipo.EspacoToken, inicio, texto, valor);
            }

            if (Atual == '+')
                return new SintaxeToken(SintaxeTipo.AdicaoToken, _posicao++, "+", null);
            else if (Atual == '-')
                return new SintaxeToken(SintaxeTipo.SubtracaoToken, _posicao++, "-", null);
            else if (Atual == '*')
                return new SintaxeToken(SintaxeTipo.MultiplicacaoToken, _posicao++, "*", null);
            else if (Atual == '/')
                return new SintaxeToken(SintaxeTipo.DivisaoToken, _posicao++, "/", null);
            else if (Atual == '(')
                return new SintaxeToken(SintaxeTipo.AbreParentesisToken, _posicao++, "(", null);
            else if (Atual == ')')
                return new SintaxeToken(SintaxeTipo.FechaParentesesToken, _posicao++, ")", null);
            return new SintaxeToken(SintaxeTipo.BadToken, _posicao++, _texto.Substring(_posicao -1,1), null);
        }
    }
}