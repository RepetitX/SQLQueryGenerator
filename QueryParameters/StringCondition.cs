using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLQueryGenerator.Extensions;

namespace SQLQueryGenerator.QueryParameters
{
    public class StringCondition : BaseCondition
    {
        public StringCondition(StringQueryField Field, CompareCondition Condition, string Value)
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                queryPart = "";
                return;
            }

            queryPart = $"{Field.GetQueryPart()} {Condition.GetSign()} {Value}";
        }
    }
}
