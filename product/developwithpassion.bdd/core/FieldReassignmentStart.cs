using System;
using System.Linq.Expressions;
using System.Reflection;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public class FieldReassignmentStart
    {
        FieldSwitcherFactory field_switcher_factory;

        public FieldReassignmentStart() : this(new FieldSwitcherFactoryImplementation()) {}

        public FieldReassignmentStart(FieldSwitcherFactory field_switcher_factory)
        {
            this.field_switcher_factory = field_switcher_factory;
        }

        public FieldSwitcher change(Expression<Func<object>> field_to_change)
        {
            return field_switcher_factory.create_to_target(get_member_from(field_to_change));
        }


        MemberInfo get_member_from(Expression<Func<object>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var result = expression.Body.downcast_to<UnaryExpression>().Operand;
                return result.downcast_to<MemberExpression>().Member;
            }
            var member_expression = expression.Body.downcast_to<MemberExpression>();
            return member_expression.Member;
        }
    }
}