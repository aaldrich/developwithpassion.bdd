using System.Reflection;

namespace developwithpassion.bdd.core
{
    public class PropertyInfoMemberTarget : MemberTarget
    {
        PropertyInfo member;

        public PropertyInfoMemberTarget(MemberInfo member)
        {
            this.member = member.DeclaringType.GetProperty(member.Name);
        }

        public object get_value()
        {
            return member.GetValue(member.DeclaringType, new object[0]);
        }

        public void change_value_to(object new_value)
        {
            member.SetValue(member.DeclaringType, new_value, new object[0]);
        }
    }
}