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
            stringBuilder.Append("SELECT * FROM Tabela");

            stringBuilder.Append(GetWhereClause(Filtros[i]));

            if (IsUltimaPosicao(i))
            {
                stringBuilder.Append(" UNION ");
            }
            
        }

        private bool IsUltimaPosicao(int i)
        {
            return i < Filtros.Count - 1;
        }

        public string GetWhereClause(T filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var props = typeof(T).GetProperties();

            if (props.Length == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            sb.Append(" WHERE ");

            for (var i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                if (prop.GetValue(filter) == null)
                {
                    continue;
                }

                if (i != 0)
                {
                    sb.Append(" AND ");
                }

                sb.Append(prop.Name);
                sb.Append(" = ");
                
                if (prop.PropertyType.IsEquivalentTo(typeof(string)))
                {
                    sb.Append('\'');
                    sb.Append(prop.GetValue(filter));
                    sb.Append('\'');
                } else
                {
                    sb.Append(prop.GetValue(filter));
                }
            }

            return sb.ToString();
        }
    }
}
