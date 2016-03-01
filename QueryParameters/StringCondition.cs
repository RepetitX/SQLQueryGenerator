using System.Collections.Generic;
using System.Linq;
using SQLQueryGenerator.Extensions;

namespace SQLQueryGenerator.QueryParameters
{
    public class StringCondition : BaseCondition
    {
        public StringCondition(StringQueryField Field, CompareCondition Condition, string Value)
        {
            Value = Value.Replace("'", "''");
            if (string.IsNullOrWhiteSpace(Value))
            {
                queryPart = "";
                return;
            }

            queryPart = $"{Field.GetQueryPart()} {Condition.GetSign()} '{Value}'";
        }

        public StringCondition(StringQueryField Field, ListCondition Condition, IEnumerable<string> Values)
        {
            if (Values == null)
            {
                queryPart = "";
                return;
            }

            string values = string.Join(", ", Values.Select(val => $"'{val.Replace("'", "''")}'"));

            if (string.IsNullOrWhiteSpace(values))
            {
                queryPart = "";
            }
            else
            {
                queryPart = $"{Field.GetQueryPart()} {(Condition == ListCondition.In ? "in" : "not in")} ({values})";
            }
        }
    }
}