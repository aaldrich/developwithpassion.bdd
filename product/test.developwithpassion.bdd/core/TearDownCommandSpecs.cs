using System;
using System.Collections.Generic;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd.core
{
    public class TearDownCommandSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<Command,
                                            TearDownCommand<int>> {}

        [Concern(typeof (TearDownCommand<int>))]
        public class when_tearing_down_a_test : concern
        {
            context c = () =>
            {
                state = an<TestState<int>>();
                state.dependencies = new Dictionary<Type, object>();
                controller = an<DelegateController>();
            };

            public override Command create_sut()
            {
                return new TearDownCommand<int>(state, controller);
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

            static DelegateController controller;
            static TestState<int> state;
        }
    }
}