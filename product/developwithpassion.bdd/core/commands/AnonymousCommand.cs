using System;

namespace developwithpassion.bdd.core.commands
{
    public class AnonymousCommand : Command
    {
        Action action;

        public AnonymousCommand(Action action)
        {
            this.action = action;
        }

        public void run()
        {
            action();
        }
    }
}