
using System.Linq;
using System.Reflection;
using System.Text;

namespace GeradorUnion
{
    public class WhereClauseGenerator<T>
    {
        private readonly T _modelForWhere;
        private readonly StringBuilder _stringBuilder;
        public WhereClauseGenerator(T modelForWhere)
        {
            _modelForWhere = modelForWhere;
            _stringBuilder = new StringBuilder();
        }

        public string GetWhereClause()
        {

            _stringBuilder.Clear();

            var props = GetPropertyInfos();

            if (IsValidProps(props))
            {
                return string.Empty;
            }

            PopulateStringBuilder(props);

            return _stringBuilder.ToString();
        }

        

        private PropertyInfo[]? GetPropertyInfos()
        {
            if (_modelForWhere == null)
            {
                return null;
            }

            var props = typeof(T).GetProperties();

            return props;
        }

        private void AddPropInString(PropertyInfo? prop)
        {
            if (prop?.GetValue(_modelForWhere) == null)
            {
                return;
            }

            AddColumnInString(prop);
            AddValueInString(prop);
        }

        private void AddValueInString(PropertyInfo prop)
        {
            if (IsPropString(prop))
            {
                _stringBuilder.Append('\'');
                _stringBuilder.Append(prop.GetValue(_modelForWhere));
                _stringBuilder.Append('\'');
            }
            else
            {
                _stringBuilder.Append(prop.GetValue(_modelForWhere));
            }
        }

        private void AddColumnInString(PropertyInfo prop)
        {
            _stringBuilder.Append(prop.Name);
            _stringBuilder.Append(" = ");
        }

        private void AddAndKeyword(PropertyInfo prop)
        {
            if (prop?.GetValue(_modelForWhere) == null)
            {
                return;
            }

            _stringBuilder.Append(" AND ");
        }

        private static bool IsPropString(PropertyInfo prop)
        {
            return prop.PropertyType.IsEquivalentTo(typeof(string));
        }
    
        private bool IsValidProps(PropertyInfo[]? props)
        {
            if (props == null || props.Length == 0)
            {
                return true;
            }

            return props.All(prop => prop?.GetValue(_modelForWhere) == null);
        }
        
        private void PopulateStringBuilder(PropertyInfo[]? props)
        {
            _stringBuilder.Append(" WHERE ");

            AddPropInString(props[0]);

            for (var i = 1; i < props.Length; i++)
            {
                AddAndKeyword(props[i]);
                AddPropInString(props[i]);
            }
        }
    }
}
