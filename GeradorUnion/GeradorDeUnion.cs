using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeradorUnion
{
    public class GeradorDeUnion<T>
    {
        private readonly IList<T> Filtros;

        public GeradorDeUnion(IList<T> filtros)
        {
            Filtros = filtros;
        }

        public string Gerar()
        {
            var count = Filtros.Count;
            var sb = new StringBuilder();
            for (var i = 0; i < count; i++)
            {
                sb.Append("SELECT * FROM Tabela");
                
                sb.Append(GetWhereClause(Filtros[i]));

                if (i < count - 1)
                {
                    sb.Append(" UNION ");

                }
            }
            return sb.ToString();
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
