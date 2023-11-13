namespace lc
{
    sealed class NumberExpressionSyntax : SintaxeExpressão
    {
        public NumberExpressionSyntax(SintaxeToken numeroToken)
        {
            NumeroToken = numeroToken;
        }

        public override SintaxeTipo Tipo => SintaxeTipo.ExpressaoNumero;

        public SintaxeToken NumeroToken { get; }
    }
}
