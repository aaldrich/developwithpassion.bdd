using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.core.observations;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mocking.rhino;
using developwithpassion.bdddoc.core;
using Rhino.Mocks;

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

            it should_direct_the_test_state_to_builds_its_sut = () =>
            {
                state.received(x => x.build_sut());
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