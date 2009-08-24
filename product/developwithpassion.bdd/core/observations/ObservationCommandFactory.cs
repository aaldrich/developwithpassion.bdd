using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.commands;

namespace developwithpassion.bdd.core.observations
{
    public interface ObservationCommandFactory
    {
        Command create_reset_command();
        Command create_prepare_observations_command();
        Command create_teardown_command();
        Command create_before_all_observations_command();
        Command create_after_all_observations_command();
    }

    public class ObservationCommandFactoryImplementation<SUT> : ObservationCommandFactory
    {
        TestState<SUT> test_state;
        DelegateController delegate_controller;

        public ObservationCommandFactoryImplementation(TestState<SUT> state_implementation, DelegateController delegate_controller)
        {
            this.test_state = state_implementation;
            this.delegate_controller = delegate_controller;
        }

        public Command create_reset_command()
        {
            return new ResetTestState<SUT>(test_state);
        }

        public Command create_prepare_observations_command()
        {
            return new PrepareToMakeAnObservation<SUT>(test_state,delegate_controller );
        }

        public Command create_teardown_command()
        {
            return new TearDown<SUT>(test_state, delegate_controller);
        }

        public Command create_before_all_observations_command()
        {
            return new AnonymousCommand(() => delegate_controller.run_block<before_all_observations>());
        }

        public Command create_after_all_observations_command()
        {
            return new AnonymousCommand(() => delegate_controller.run_block<after_all_observations>());
        }
    }
}