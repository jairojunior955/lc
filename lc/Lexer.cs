using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lc
{
    internal class Lexer
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
                return new SintaxeToken(SintaxeTipo.EspacoToken, inicio, texto, null);
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
            else if (Atual == ';')
                return new SintaxeToken(SintaxeTipo.SeparadorToken, _posicao++, ";", null);
            return new SintaxeToken(SintaxeTipo.BadToken, _posicao++, _texto.Substring(_posicao -1,1), null);
        }
    }
}
