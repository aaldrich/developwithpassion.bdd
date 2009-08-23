using System;
using System.Linq;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public class SystemUnderTestDependencyBuilderImplementation : SystemUnderTestDependencyBuilder
    {
        BasicTestState test_state;
        MockFactory mock_factory;

        public SystemUnderTestDependencyBuilderImplementation(BasicTestState test_state, MockFactory mock_factory)
        {
            this.test_state = test_state;
            this.mock_factory = mock_factory;
        }

        public object get_the_provided_dependency_assignable_from(Type constructor_parament_type)
        {
            return test_state.dependencies[constructor_parament_type];
        }

        public bool dependency_needs_to_be_registered_for(Type dependency_type)
        {
            return does_not_have_dependency_registered_for(dependency_type) &&
                   is_a_depedency_that_can_automatically_be_created(dependency_type);
        }

        public bool is_a_depedency_that_can_automatically_be_created(Type dependency_type)
        {
            return ! dependency_type.IsValueType;
        }

        public bool does_not_have_dependency_registered_for(Type dependency_type)
        {
            return ! test_state.dependencies.ContainsKey(dependency_type);
        }

        public void register_dependency_for_sut(Type dependency_type)
        {
            test_state.dependencies[dependency_type] = mock_factory.create_stub(dependency_type);
        }


        public Dependency the_dependency<Dependency>() where Dependency : class
        {
            if (has_no_dependency_for<Dependency>()) test_state.dependencies[typeof (Dependency)] = mock_factory.create_stub<Dependency>();

            return (Dependency) test_state.dependencies[typeof (Dependency)];
        }

        public Dependency provide_an<Dependency>(Dependency instance) where Dependency : class
        {
            store_a_sut_constructor_argument(instance);
            return instance;
        }

        void store_a_sut_constructor_argument<ArgumentType>(ArgumentType argument)
        {
            if (! has_no_dependency_for<ArgumentType>())
                throw new ArgumentException(
                    "A dependency has already been provided for :{0}".format_using(typeof (ArgumentType).proper_name()));
            test_state.dependencies[typeof (ArgumentType)] = argument;
        }

        public void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            store_a_sut_constructor_argument(value);
        }

        public object[] all_dependencies()
        {
            return test_state.dependencies.Values.ToArray();
        }

        public bool has_no_dependency_for<Interface>()
        {
            return does_not_have_dependency_registered_for(typeof (Interface));
        }
    }
}