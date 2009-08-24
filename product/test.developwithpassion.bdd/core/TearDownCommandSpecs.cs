using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.core.observations;
using developwithpassion.bdd.harnesses.mbunit;
using developwithpassion.bdd.mocking.rhino;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd.core
{
    public class TearDownCommandSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<Command,
                                            TearDown<int>> {}

        [Concern(typeof (TearDown<int>))]
        public class when_tearing_down_a_test : concern
        {
            context c = () =>
            {
                state = an<TestState<int>>();
                controller = an<DelegateController>();
            };

            public override Command create_sut()
            {
                return new TearDown<int>(state, controller);
            }

            because b = () =>
            {
                sut.run();
            };

            it should_run_all_of_the_after_each_observation_blocks = () =>
            {
                controller.received(x => x.run_block<after_each_observation>());
            };

            it should_run_all_of_the_teardown_pipeline_blocks = () =>
            {
                state.received(x => x.run_teardown_pipeline());
            };

            it should_clear_all_of_the_test_dependencies = () =>
            {
                state.received(x => x.empty_dependencies());
            };

            static DelegateController controller;
            static TestState<int> state;
        }
    }
}