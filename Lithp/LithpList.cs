using System;
using System.Collections.Generic;
using Lithp.Functions;

namespace Lithp
{
    public class LithpList
    {
        public Function Function { get; }
        public IList<LithpListItem> Items { get; } = new List<LithpListItem>();

        public LithpList(Function function)
        {
            Function = function ?? throw new ArgumentNullException(nameof(function));
        }

        public void Add(LithpListItem item)
        {
            Items.Add(item);
        }

        public object Evaluate()
        {
            var itemValues = new object[Items.Count];
            for (var i = 0; i < Items.Count; i++)
            {
                itemValues[i] = Function.DeferChildExecution
                    ? Items[i].Value()
                    : Items[i].Evaluate();
            }

            object result;
            if (!(Function is ReturnFunction) && itemValues.Length == 1 && itemValues[0] is object[] listValues)
                result = Function.Execute(listValues);
            else
                result = Function.Execute(itemValues);

            return result;
        }
    }

    public class LithpListItem
    {
        public Expression Expression { get; }
        public LithpList ChildList { get; }

        public LithpListItem(Expression expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public LithpListItem(LithpList childList)
        {
            ChildList = childList ?? throw new ArgumentNullException(nameof(childList));
        }

        public object Evaluate()
        {
            var result = Expression != null
                ? Expression.Evaluate()
                : ChildList.Evaluate();

            return result;
        }

        public object Value()
        {
            if (Expression != null)
                return Expression;

            return ChildList;
        }
    }
}