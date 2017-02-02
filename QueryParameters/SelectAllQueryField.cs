using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLQueryGenerator.QueryParameters
{
    public class SelectAllQueryField : IQueryField
    {      
        public string Expression { get; set; }
        public string Alias { get; set; }
        public string GetQueryPart()
        {
            return Expression;
        }

        public SelectAllQueryField(string Expression)
        {
            this.Expression = Expression;
        }

        public string GetTypeName()
        {
            return "";
        }
    }
}
