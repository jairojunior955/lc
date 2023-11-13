namespace lc
{
    sealed class BinaryExpressionSyntax : SintaxeExpressão
    {
        public BinaryExpressionSyntax(SintaxeExpressão esquerda, SintaxeToken operadorToken, SintaxeExpressão direita)
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
}
