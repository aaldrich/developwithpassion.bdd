using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.core.observations;
using developwithpassion.bdd.harnesses.mbunit;
using developwithpassion.bdd.mocking.rhino;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd.core
{
    public class ResetTestStateSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<Command,
                                            ResetTestState<int>>
        {
            context c = () =>
            {
                state_implementation = an<TestState<int>>();
            };

            public override Command create_sut()
            {
                return new ResetTestState<int>(state_implementation);
            }

            static protected TestState<int> state_implementation;
        }

        [Concern(typeof (ResetTestState<int>))]
        public class when_test_test_state_is_reset : concern
        {
            because b = () =>
            {
                sut.run();
            };
            it should_clear_the_dependencies_dictionary = () =>
            {
                state_implementation.received(x => x.empty_dependencies());
            };

            it should_clear_the_test_pipeline = () =>
            {
                state_implementation.received(x => x.clear_test_pipeline());
            };

            it should_add_the_unit_test_container_teardown_behaviour = () =>
            {
                state_implementation.received(x => x.add_pipeline_behaviour(CommonPipelineBehaviours.tear_down_the_unit_test_container));
            };
        }
    }
}