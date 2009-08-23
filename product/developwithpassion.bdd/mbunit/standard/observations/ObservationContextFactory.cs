using developwithpassion.bdd.core;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    public interface ObservationContextFactory
    {
        ObservationContext<Contract> create_from<Contract>(ObservationContextArgs<Contract> args);
    }

    public class ObservationContextFactoryImplementation : ObservationContextFactory
    {
        public ObservationContext<Contract> create_from<Contract>(ObservationContextArgs<Contract> args)
        {
            var dependency_builder = new SystemUnderTestDependencyBuilderImplementation(args.state, args.mock_factory);
            return new ObservationContext<Contract>(
                args.state,
                new ObservationCommandFactoryImplementation<Contract>(args.state, new DelegateControllerImplementation(args.test)),
                args.mock_factory,
                dependency_builder,
                new SystemUnderTestFactoryImplementation(dependency_builder));
        }
    }

    public class ObservationCommandFactoryFactory
    {
        ObservationCommandFactory create_from<Contract>(ObservationContextArgs<Contract> args)
        {
            return new ObservationCommandFactoryImplementation<Contract>(args.state, new DelegateControllerImplementation(args.test));
        }
    }
}