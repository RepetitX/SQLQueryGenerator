using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLQueryGenerator.QueryParameters
{
    public enum ConditionGroupType
    {
        And,
        Or
    }

    public class ConditionGroup : IQueryCondition
    {
        public ConditionGroupType Type { get; set; }

        protected List<IQueryCondition> conditions;
        protected QueryFieldsContainer fieldsContainer;

        public bool IsEmpty
        {
            get { return conditions.All(cnd => cnd.IsEmpty); }
        }

        public ConditionGroup(ConditionGroupType Type, QueryFieldsContainer FieldsContainer)
        {
            this.Type = Type;
            conditions = new List<IQueryCondition>();
            fieldsContainer = FieldsContainer;
        }

        public void AddCondition(IQueryCondition Condition)
        {
            conditions.Add(Condition);
        }

        public void AddCondition<T>(string FieldName, CompareCondition Comparsion, T? Value) where T : struct
        {
            QueryField<T> field = fieldsContainer.GetQueryField<T>(FieldName);
            conditions.Add(new QueryCondition<T>(field, Comparsion, Value));
        }

        public void AddCondition(string FieldName, CompareCondition Comparsion, string Value)
        {
            StringQueryField field = (StringQueryField) fieldsContainer.GetField(FieldName);
            conditions.Add(new StringCondition(field, Comparsion, Value));
        }
        public void AddCondition(string FieldName, StringCompareCondition Comparsion, string Value)
        {
            StringQueryField field = (StringQueryField)fieldsContainer.GetField(FieldName);
            conditions.Add(new StringCondition(field, Comparsion, Value));
        }

        public void AddCondition<T>(string FieldName, NullCondition Condition) where T : struct
        {
            QueryField<T> field = fieldsContainer.GetQueryField<T>(FieldName);
            conditions.Add(new QueryCondition<T>(field, Condition));
        }
        public void AddCondition(string FieldName, NullCondition Condition)
        {
            StringQueryField field = (StringQueryField)fieldsContainer.GetField(FieldName);
            conditions.Add(new BaseCondition(field, Condition));
        }

        public void AddCondition<T>(string FieldName, ListCondition Condition, IEnumerable<T> Values) where T : struct
        {
            QueryField<T> field = fieldsContainer.GetQueryField<T>(FieldName);
            conditions.Add(new QueryCondition<T>(field, Condition, Values));
        }

        public void AddCondition(string FieldName, ListCondition Condition, IEnumerable<string> Values)
        {
            StringQueryField field = (StringQueryField) fieldsContainer.GetField(FieldName);
            conditions.Add(new StringCondition(field, Condition, Values));
        }

        public ConditionGroup AddConditionGroup(ConditionGroupType ConditionType)
        {
            ConditionGroup group = new ConditionGroup(ConditionType, fieldsContainer);
            AddCondition(group);
            return group;
        }

        public string GetQueryPart()
        {
            string condtions = string.Join(Type == ConditionGroupType.And ? "\nand " : "\nor ",
                conditions.Where(cnd => !cnd.IsEmpty).Select(cnd => cnd.GetQueryPart()));

            if (string.IsNullOrWhiteSpace(condtions))
            {
                return "";
            }
            else
            {
                return $"({condtions})";
            }
        }
    }
}