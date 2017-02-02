using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLQueryGenerator.Queries;

namespace SQLQueryGenerator.QueryParameters
{
    public enum ExistCondidion
    {
        Exists,
        NotExists
    }

    public class SubQueryCondition : IQueryCondition
    {
        private string queryPart;

        public SubQueryCondition(SelectQuery Query, ExistCondidion Condition)
        {
            string queryString = Query.GetQueryString();
            if (!string.IsNullOrWhiteSpace(queryString))
            {
                queryPart = $"{(Condition == ExistCondidion.Exists ? "exists" : "not exists")} ({queryString})";
            }
        }

        public string GetQueryPart()
        {
            return queryPart;
        }

        public bool IsEmpty => string.IsNullOrWhiteSpace(queryPart);
    }
}
