using System.Reflection;

namespace developwithpassion.bdd.core
{
    public class FieldMemberTarget : MemberTarget
    {
        FieldInfo member;

        public FieldMemberTarget(MemberInfo member_info)
        {
            this.member = member_info.DeclaringType.GetField(member_info.Name);
        }

        public object get_value()
        {
            return member.GetValue(member.DeclaringType);
        }

        public void change_value_to(object new_value)
        {
            member.SetValue(member.DeclaringType, new_value);
        }
    }
}