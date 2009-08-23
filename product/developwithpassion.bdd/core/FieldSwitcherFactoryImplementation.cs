using System.Reflection;

namespace developwithpassion.bdd.core
{
    public class FieldSwitcherFactoryImplementation : FieldSwitcherFactory
    {
        MemberTargetRegistry member_target_registry;

        public FieldSwitcherFactoryImplementation() :this(new MemberTargetRegistryImplementation()){}

        public FieldSwitcherFactoryImplementation(MemberTargetRegistry member_target_registry)
        {
            this.member_target_registry = member_target_registry;
        }

        public FieldSwitcher create_to_target(MemberInfo member)
        {
            return new FieldSwitcherImplementation(member_target_registry.get_member_target_for(member));
        }
    }
}