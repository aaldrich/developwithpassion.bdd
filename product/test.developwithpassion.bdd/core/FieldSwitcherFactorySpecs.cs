using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.harnesses.mbunit;
using developwithpassion.bdddoc.core;
using Rhino.Mocks;

namespace test.developwithpassion.bdd.core
{
    public class FieldSwitcherFactorySpecs
    {
        public abstract class concern : observations_for_a_sut_without_a_contract<FieldSwitcherFactoryImplementation>
        {
            context c = () =>
            {
                member = typeof (Item).GetProperty("static_value");
                registry = the_dependency<MemberTargetRegistry>();
                member_target = an<MemberTarget>();
                member_target.Stub(x => x.get_value()).Return("Blah");

                registry.Stub(x => x.get_member_target_for(member)).Return(member_target);
            };

            static protected PropertyInfo member;
            static protected MemberTargetRegistry registry;
            static MemberTarget member_target;
        }

        [Concern(typeof (FieldSwitcherFactoryImplementation))]
        public class when_creating_a_field_switcher : concern
        {
            because b = () =>
            {
                result = sut.create_to_target(member);
            };

            it should_use_the_member_target_registry_to_create_a_target_to_target_the_member_type = () =>
            {
                result.should_be_an_instance_of<FieldSwitcherImplementation>();
            };

            static FieldSwitcher result;
        }
    }

    public class Item
    {
        static public string static_value { get; set; }
    }
}