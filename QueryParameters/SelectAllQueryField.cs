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
            Alias = Expression;
        }

        public string GetTypeName()
        {
            return "";
        }
    }
}
