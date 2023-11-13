namespace lc
{
    internal class Parser
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
                esquerda = new BinaryExpressionSyntax(esquerda, operadorToken, direita);
            }

            return esquerda;
        }

        private SintaxeExpressão ExpressaoParsePrimaria()
        {
            var numeroToken = Combinado(SintaxeTipo.NumeroToken);
            return new NumberExpressionSyntax(numeroToken);
        }
    }
}
