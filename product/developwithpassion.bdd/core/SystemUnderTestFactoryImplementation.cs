using System;
using System.Linq;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public class SystemUnderTestFactoryImplementation : SystemUnderTestFactory
    {
        SystemUnderTestDependencyBuilder dependency_builder;

        public SystemUnderTestFactoryImplementation(SystemUnderTestDependencyBuilder dependency_builder)
        {
            this.dependency_builder = dependency_builder;
        }

        public Contract create<Contract, Class>()
        {
            var constructor = typeof (Class).greediest_constructor();
            var constructor_parameter_types = constructor.GetParameters().Select(constructor_arg => constructor_arg.ParameterType);

            constructor_parameter_types.each(dependency_type =>
            {
                if (dependency_builder.dependency_needs_to_be_registered_for(dependency_type))
                    dependency_builder.register_dependency_for_sut(dependency_type);
            });

            return (Contract) Activator.CreateInstance(typeof (Class), dependency_builder.all_dependencies());
        }
    }
}