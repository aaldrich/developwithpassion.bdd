using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd
{
    public class FieldSwitcherImplementationSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<FieldSwitcher, FieldSwitcherImplementation> {}

        [Concern(typeof (FieldSwitcher))]
        public class when_provided_the_value_to_change_to : concern
        {
            context c = () =>
            {
                value_to_change_to = "sdfsdf";
                provide_a_basic_sut_constructor_argument(typeof(Item));
                provide_a_basic_sut_constructor_argument(typeof(Item).GetField("static_value",BindingFlags.Public | BindingFlags.Static));
            };

            because b = () =>
            {
                result = sut.to(value_to_change_to);
            };


            it should_provide_the_pipeline_pair_that_can_do_the_switching = () =>
            {
                result.start();
                Item.static_value.should_be_equal_to(value_to_change_to);

                result.finish();
                Item.static_value.should_be_equal_to("lah");
            };

            static PipelineBehaviour result;
            static string value_to_change_to;
        }

        public class Item
        {
            static public string static_value = "lah";
        }
    }
}