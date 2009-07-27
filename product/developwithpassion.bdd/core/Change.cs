using System;
using System.Linq.Expressions;

namespace developwithpassion.bdd.core
{
    public class Change
    {
        static public FieldSwitcher the(Expression<Func<object>> static_expression)
        {
            return new FieldReassignmentStart().change(static_expression);
        }
    }
}