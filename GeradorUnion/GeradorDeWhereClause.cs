
using System.Linq;
using System.Reflection;
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


            if (IsEmptyObject(props))
            {
                return string.Empty;
            }

            _stringBuilder.Append(" WHERE ");

            AdicionarPropriedadeNaString(props[0]);

            for (var i = 1; i < props.Length; i++)
            {
                AdicionarAndNaString(props[i]);
                AdicionarPropriedadeNaString(props[i]);
            }

            return _stringBuilder.ToString();
        }

        private void AdicionarPropriedadeNaString(PropertyInfo? prop)
        {
            if (prop?.GetValue(ModeloParaWhere) == null)
            {
                return;
            }

            AdicionarColunaNaString(prop);
            AdicionarValorNaString(prop);
        }

        private void AdicionarValorNaString(PropertyInfo prop)
        {
            if (IsPropString(prop))
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

        private void AdicionarColunaNaString(PropertyInfo prop)
        {
            _stringBuilder.Append(prop.Name);
            _stringBuilder.Append(" = ");
        }

        private void AdicionarAndNaString(PropertyInfo prop)
        {
            if (prop?.GetValue(ModeloParaWhere) == null)
            {
                return;
            }

            _stringBuilder.Append(" AND ");
        }

        private static bool IsPropString(PropertyInfo prop)
        {
            return prop.PropertyType.IsEquivalentTo(typeof(string));
        }
    
        private bool IsEmptyObject(PropertyInfo[] props)
        {
            if (props.Length == 0)
            {
                return true;
            }

            return props.All(prop => prop?.GetValue(ModeloParaWhere) == null);
        }
    }
}
