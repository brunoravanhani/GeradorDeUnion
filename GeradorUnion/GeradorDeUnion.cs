using System;
using System.Collections.Generic;
using System.Text;

namespace GeradorUnion
{
    public class GeradorDeUnion<T>
    {
        private readonly IList<T> _filters;
        private readonly StringBuilder _stringBuilder;

        public GeradorDeUnion(IList<T> filters)
        {
            _filters = filters;
            _stringBuilder = new StringBuilder();
        }

        public string Gerar()
        {
            _stringBuilder.Clear();
            
            for (var i = 0; i < _filters.Count; i++)
            {
                PopulateStringBuilder(i);
            }
            return _stringBuilder.ToString();
        }

        private void PopulateStringBuilder(int i)
        {
            var geradorDeWhere = new GeradorDeWhereClause<T>(_filters[i]);

            _stringBuilder.Append("SELECT * FROM Table");

            _stringBuilder.Append(geradorDeWhere.GetWhereClause());

            if (IsLastPosition(i))
            {
                _stringBuilder.Append(" UNION ");
            }
            
        }

        private bool IsLastPosition(int i)
        {
            return i < _filters.Count - 1;
        }
        
    }
}
