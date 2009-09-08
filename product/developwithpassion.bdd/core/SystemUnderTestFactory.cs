using System;
using System.Linq;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public interface SystemUnderTestFactory
    {
        Contract create<Contract,Class>(); 
    }

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
            constructor_parameter_types.each(dependency_builder.register_only_if_missing);
            return (Contract) Activator.CreateInstance(typeof (Class), dependency_builder.all_dependencies(constructor_parameter_types));
        }
    }
}