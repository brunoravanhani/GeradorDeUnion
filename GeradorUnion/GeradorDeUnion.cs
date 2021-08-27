using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeradorUnion
{
    public class GeradorDeUnion<T>
    {
        private readonly IList<T> Filtros;
        private StringBuilder stringBuilder;

        public GeradorDeUnion(IList<T> filtros)
        {
            Filtros = filtros;
            stringBuilder = new StringBuilder();
        }

        public string Gerar()
        {
            stringBuilder.Clear();
            
            for (var i = 0; i < Filtros.Count; i++)
            {
                PopularStringBuilderComScripts(i);
            }
            return stringBuilder.ToString();
        }

        private void PopularStringBuilderComScripts(int i)
        {
            var geradorDeWhere = new GeradorDeWhereClause<T>(Filtros[i]);

            stringBuilder.Append("SELECT * FROM Tabela");

            stringBuilder.Append(geradorDeWhere.GetWhereClause());

            if (IsUltimaPosicao(i))
            {
                stringBuilder.Append(" UNION ");
            }
            
        }

        private bool IsUltimaPosicao(int i)
        {
            return i < Filtros.Count - 1;
        }
        
    }
}
