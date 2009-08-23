using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd.core
{
    public class PropertyInfoTargetSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<MemberTarget,
                                            PropertyInfoMemberTarget>
        {
            context c = () =>
            {
                original_value = "original";
                Item.static_value = original_value;
                member = typeof (Item).GetProperty("static_value");
                member.should_not_be_null();
                provide_a_basic_sut_constructor_argument(member);
            };

            static protected MemberInfo member;
            static protected string original_value;
        }

        [Concern(typeof (PropertyInfoMemberTarget))]
        public class when_getting_its_value : concern
        {
            because b = () =>
            {
                result = sut.get_value();
            };


            it should_get_the_value_of_the_field = () =>
            {
                result.should_be_equal_to(Item.static_value);
            };

            static object result;
        }

        [Concern(typeof (FieldMemberTarget))]
        public class when_setting_its_value : concern
        {
            context c = () =>
            {
                value_to_change_to = "blasfsfd";
            };

            because b = () =>
            {
                sut.change_value_to(value_to_change_to);
            };


            it should_change_the_value_of_the_field = () =>
            {
                Item.static_value.should_be_equal_to(value_to_change_to);
            };

            static object result;
            static string value_to_change_to;
        }

        public class Item
        {
            static public string static_value { get; set; }
        }
    }
}