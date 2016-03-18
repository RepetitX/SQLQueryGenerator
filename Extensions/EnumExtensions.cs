using SQLQueryGenerator.QueryParameters;

namespace SQLQueryGenerator.Extensions
{
    public static class EnumExtensions
    {
        public static string GetSign(this CompareCondition Condition)
        {
            switch (Condition)
            {
                case CompareCondition.Equal:
                    return "=";
                case CompareCondition.Less:
                    return "<";
                case CompareCondition.LessOrEqual:
                    return "<=";
                case CompareCondition.Greater:
                    return ">";
                case CompareCondition.GreaterOrEqual:
                    return ">=";                
                case CompareCondition.NotEqual:
                default:
                    return "<>";
            }
        }
    }
}
