namespace developwithpassion.bdd.core.commands
{
    public class ChainedCommand : Command
    {
        private readonly Command first_command;
        private readonly Command second_command;

        public ChainedCommand(Command first_command, Command second_command)
        {
            this.first_command = first_command;
            this.second_command = second_command;
        }

        public void run()
        {
            first_command.run();
            second_command.run();
        }
    }
}