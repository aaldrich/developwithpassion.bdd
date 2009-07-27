using System;
using System.Linq.Expressions;
using System.Reflection;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public class FieldReassignmentStart
    {
        static public FieldSwitcherFactory field_switcher_factory = (type, field) => new FieldSwitcherImplementation(type, field);

        BindingFlags binding_flags = BindingFlags.Static | BindingFlags.Public;

        public FieldSwitcher change(Expression<Func<object>> field_to_change)
        {
            var member = get_member_from(field_to_change);
            return field_switcher_factory(member.DeclaringType, member.DeclaringType.GetField(member.Name, binding_flags));
        }

        MemberInfo get_member_from(Expression<Func<object>> expression)
        {
            var member_expression = expression.Body.downcast_to<MemberExpression>();
            return member_expression.Member;
        }
    }
}