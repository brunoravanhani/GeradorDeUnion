
using System.Text;

namespace GeradorUnion
{
    public class GeradorDeWhereClause<T>
    {
        private readonly T ModeloParaWhere;
        private StringBuilder _stringBuilder;
        public GeradorDeWhereClause(T modeloParaWhere)
        {
            ModeloParaWhere = modeloParaWhere;
            _stringBuilder = new StringBuilder();
        }

        public string GetWhereClause()
        {

            _stringBuilder.Clear();

            if (ModeloParaWhere == null)
            {
                return string.Empty;
            }

            var props = typeof(T).GetProperties();

            if (props.Length == 0)
            {
                return string.Empty;
            }

            _stringBuilder.Append(" WHERE ");

            for (var i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                if (prop.GetValue(ModeloParaWhere) == null)
                {
                    continue;
                }

                if (i != 0)
                {
                    _stringBuilder.Append(" AND ");
                }

                _stringBuilder.Append(prop.Name);
                _stringBuilder.Append(" = ");

                if (prop.PropertyType.IsEquivalentTo(typeof(string)))
                {
                    _stringBuilder.Append('\'');
                    _stringBuilder.Append(prop.GetValue(ModeloParaWhere));
                    _stringBuilder.Append('\'');
                }
                else
                {
                    _stringBuilder.Append(prop.GetValue(ModeloParaWhere));
                }
            }

            return _stringBuilder.ToString();
        }
    }
}
