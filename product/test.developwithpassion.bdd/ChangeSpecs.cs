using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd
{
    public class ChangeSpecs
    {
        public abstract class concern : observations_for_a_static_sut {}

        [Concern(typeof (Change))]
        public class when_a_change_is_made_within_a_context_block : concern
        {
            context c = () =>
            {
                new_value = "other_value";
                add_pipeline_behaviour(Change.the(() => SomeStatic.some_value).to(new_value));
            };


            it should_have_caused_the_change_to_the_field = () =>
            {
                SomeStatic.some_value.should_be_equal_to(new_value);
            };

            static string new_value;
        }

        public class SomeStatic
        {
            static public string some_value = "lah";
        }
    }
}