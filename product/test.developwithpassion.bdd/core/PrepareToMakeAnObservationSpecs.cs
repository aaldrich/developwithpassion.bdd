using System.Collections.Generic;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mocking.rhino;
using developwithpassion.bdddoc.core;
using Rhino.Mocks;
using developwithpassion.bdd.mbunit;

namespace test.developwithpassion.bdd.core
{
    public class PrepareToMakeAnObservationSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<Command,
                                            PrepareToMakeAnObservation<int>> {}

        [Concern(typeof (PrepareToMakeAnObservation<int>))]
        public class when_preparing_to_make_an_observation : concern
        {
            context c = () =>
            {
                state = an<TestState<int>>();
                state.factory = () => 3;
                controller = MockRepository.GenerateStub<DelegateController>();
            };

            because b = () =>
            {
                sut.run();
            };

            public override Command create_sut()
            {
                return new PrepareToMakeAnObservation<int>(state, controller);
            }

            it should_run_the_context_block = () =>
            {
                controller.received(x => x.run_block<context>());
            };

            it should_assign_the_sut_to_the_test_state = () =>
            {
                state.sut.should_be_equal_to(3);
            };

            it should_run_the_because_blocks = () =>
            {
                controller.received(x => x.run_block<because>());
            };

            it should_not_run_the_after_each_observation_block = () =>
            {
                controller.never_received(x => x.run_block<after_each_observation>());
            };

            static DelegateController controller;
            static TestState<int> state;
        }
    }
}