using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd
{
    public class MemberTargetRegistrySpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<MemberTargetRegistry,
                                            MemberTargetRegistryImplementation>
        {
            context c = () =>
            {
                property = typeof (Item).GetProperty("static_property");
                field = typeof (Item).GetField("static_value");
            };

            static protected MemberInfo property;
            static protected MemberInfo field;
        }

        [Concern(typeof (MemberTargetRegistryImplementation))]
        public class when_getting_a_member_target_for_a_member_that_represents_a_property : concern
        {
            because b = () =>
            {
                result = sut.get_member_target_for(property);
            };


            it should_get_a_field_member_target = () =>
            {
                result.should_be_an_instance_of<PropertyInfoMemberTarget>();
            };

            static MemberTarget result;
        }
        [Concern(typeof (MemberTargetRegistryImplementation))]
        public class when_getting_a_member_target_for_a_member_that_represents_a_field : concern
        {
            because b = () =>
            {
                result = sut.get_member_target_for(field);
            };


            it should_get_a_field_member_target = () =>
            {
                result.should_be_an_instance_of<FieldMemberTarget>();
            };

            static MemberTarget result;
        }

        public class Item
        {
            static public string static_property { get; set; }
            static public string static_value = "blah";
        }
    }
}