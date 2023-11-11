using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lc
{
    internal class SintaxeToken
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
}
