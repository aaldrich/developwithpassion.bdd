using System.Reflection;

namespace developwithpassion.bdd.core
{
    public interface MemberTargetRegistry
    {
        MemberTarget get_member_target_for(MemberInfo member);
    }
}