using System.Reflection;

namespace developwithpassion.bdd.core
{
    public interface FieldSwitcherFactory {
        FieldSwitcher create_to_target(MemberInfo member);
    }
}