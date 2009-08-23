using developwithpassion.bdd.core.commands;

namespace developwithpassion.bdd.core
{
    public interface ObservationCommandFactory
    {
        Command create_reset_command();
        Command create_prepare_observations_command();
        Command create_teardown_command();
        Command create_before_all_observations_command();
        Command create_after_all_observations_command();
    }
}