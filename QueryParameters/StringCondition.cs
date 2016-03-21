using System.Collections.Generic;
using System.Linq;
using SQLQueryGenerator.Extensions;

namespace SQLQueryGenerator.QueryParameters
{
    public enum StringCompareCondition
    {
        BeginsWith,
        EndsWith,
        Contains
    }

    public class StringCondition : BaseCondition
    {
        public StringCondition(StringQueryField Field, CompareCondition Condition, string Value)
        {            
            if (string.IsNullOrWhiteSpace(Value))
            {
                queryPart = "";
                return;
            }
            Value = Value.Replace("'", "''");

            queryPart = $"{Field.GetQueryPart()} {Condition.GetSign()} '{Value}'";
        }

        public StringCondition(StringQueryField Field, StringCompareCondition Condition, string Value)
        {            
            if (string.IsNullOrWhiteSpace(Value))
            {
                queryPart = "";
                return;
            }
            Value = Value.Replace("'", "''");

            switch (Condition)
            {
                case StringCompareCondition.BeginsWith:
                    queryPart = $"{Field.GetQueryPart()} like '{Value}%'";
                    break;
                case StringCompareCondition.Contains:
                    queryPart = $"{Field.GetQueryPart()} like '%{Value}%'";
                    break;
                case StringCompareCondition.EndsWith:
                    queryPart = $"{Field.GetQueryPart()} like '%{Value}'";
                    break;
            }
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