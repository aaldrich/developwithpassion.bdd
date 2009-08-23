using developwithpassion.bdd.core.commands;

namespace developwithpassion.bdd.core
{
    public class ResetTestState<SUT> : Command
    {
        TestState<SUT> test_state;

        public ResetTestState(TestState<SUT> test_state)
        {
            this.test_state = test_state;
        }

        public void run()
        {
            test_state.clear_test_pipeline();
            test_state.add_pipeline_behaviour(CommonPipelineBehaviours.tear_down_the_unit_test_container);
            test_state.empty_dependencies();
        }
    }
}