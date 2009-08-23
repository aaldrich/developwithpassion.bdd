using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public class PrepareToMakeAnObservation<SUT> : Command
    {
        TestState<SUT> test_state;
        DelegateController controller;


        public PrepareToMakeAnObservation(TestState<SUT> test_state, DelegateController controller)
        {
            this.test_state = test_state;
            this.controller = controller;
        }

        public void run()
        {
            controller.run_block<context>();
            test_state.run_startup_pipeline();
            test_state.sut = test_state.factory();
            controller.run_block<after_the_sut_has_been_created>();
            controller.run_block<because>();
        }
    }
}