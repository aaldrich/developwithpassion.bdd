using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;

namespace developwithpassion.bdd.core.commands
{
    public class ChainedCommandSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<Command, ChainedCommand>
        {
            static protected Command command_one;
            static protected Command command_two;

            context c = () =>
            {
                command_one = an<Command>();
                command_two = an<Command>();
            };

            public override Command create_sut()
            {
                return new ChainedCommand(command_one, command_two);
            }
        }

        [Concern(typeof (ChainedCommand))]
        public class when_it_runs : concern
        {
            because b = () => sut.run();

            it should_run_each_command_it_is_composed_with = () =>
            {
                command_one.received(x => x.run());
                command_two.received(x => x.run());
            };
        }
    }
}