using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeradorUnion
{
    public class GeradorDeUnion
    {

        public string Gerar(string[] caracteristicas)
        {
            var sb = new StringBuilder();
            var count = caracteristicas.Length;
            for (var i = 0; i < count; i++)
            {
                sb.Append("Select * from Tabela where caracteristica = '");
                sb.Append(caracteristicas[i]);
                sb.Append('\'');

                if (i < count - 1)
                {
                    sb.Append(" UNION ");

                }
            }
            return sb.ToString();
        }
    }
}
