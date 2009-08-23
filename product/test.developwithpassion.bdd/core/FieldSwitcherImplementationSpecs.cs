using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mocking.rhino;
using developwithpassion.bdddoc.core;
using Rhino.Mocks;

namespace test.developwithpassion.bdd.core
{
    public class FieldSwitcherImplementationSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<FieldSwitcher, FieldSwitcherImplementation> {
            static protected MemberTarget target;

            context c = () =>
            {
                target = the_dependency<MemberTarget>();
            };
        }

        [Concern(typeof (FieldSwitcher))]
        public class when_constructed : concern
        {
            context c = () =>
            {
                value_to_change_to = "sdfsdf";
            };

            because b = () =>
            {
                result = sut.to(value_to_change_to);
            };


            it should_use_the_target_to_get_the_original_value = () =>
            {
                target.received(x => x.get_value());
            };

            static PipelineBehaviour result;
            static string value_to_change_to;
        }

        [Concern(typeof (FieldSwitcher))]
        public class when_provided_the_value_to_change_to : concern
        {
            context c = () =>
            {
                value_to_change_to = "sdfsdf";
                original_value = "original value";
                target.Stub(x => x.get_value()).Return(original_value);
            };

            because b = () =>
            {
                result = sut.to(value_to_change_to);
            };


            it should_provide_the_pipeline_pair_that_can_do_the_switching = () =>
            {
                result.start();
                target.received(x => x.change_value_to(value_to_change_to));

                result.finish();
                target.received(x => x.change_value_to(original_value));
            };

            static PipelineBehaviour result;
            static string value_to_change_to;
            static string original_value;
        }

    }
}