using System.Collections.Generic;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core.commands
{
    static public class CommandExtensions
    {
        static public Command followed_by(this Command first, Command second)
        {
            return new ChainedCommand(first, second);
        }

        static public Command as_command_chain(this IEnumerable<Command> commands){
            Command chain = new NullCommand();
            commands.each(x => chain = chain.followed_by(x));
            return chain;
        }
    }
}