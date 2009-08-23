using System;
using System.Linq;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    public abstract class observations_for_an_instance_sut<ContractUnderTest, ClassUnderTest> : an_observations_set_of_basic_behaviours<ContractUnderTest> where ClassUnderTest : ContractUnderTest
    {
        public override ContractUnderTest create_sut()
        {
            var constructor = typeof (ClassUnderTest).greediest_constructor();
            var constructor_parameter_types = constructor.GetParameters().Select(constructor_arg => constructor_arg.ParameterType);

            constructor_parameter_types.each(dependency_type =>
            {
                if (dependency_needs_to_be_registered_for(dependency_type)) register_dependency_for_sut(dependency_type);
            });

            var constructor_args = constructor_parameter_types.Select(constructor_parament_type => get_the_provided_dependency_assignable_from(constructor_parament_type)).ToArray();

            return (ContractUnderTest) Activator.CreateInstance(typeof (ClassUnderTest), constructor_args);
        }

        object get_the_provided_dependency_assignable_from(Type constructor_parament_type)
        {
            return test_state.dependencies[constructor_parament_type];
        }

        static bool dependency_needs_to_be_registered_for(Type dependency_type)
        {
            return does_not_have_dependency_registered_for(dependency_type) &&
                   is_a_depedency_that_can_automatically_be_created(dependency_type);
        }

        static bool is_a_depedency_that_can_automatically_be_created(Type dependency_type)
        {
            return ! dependency_type.IsValueType;
        }

        static bool does_not_have_dependency_registered_for(Type dependency_type)
        {
            return ! test_state.dependencies.ContainsKey(dependency_type);
        }

        static void register_dependency_for_sut(Type dependency_type)
        {
            test_state.dependencies[dependency_type] = an_item_of(dependency_type);
        }


        static protected Dependency the_dependency<Dependency>() where Dependency : class
        {
            if (has_no_dependency_for<Dependency>()) test_state.dependencies[typeof (Dependency)] = an<Dependency>();

            return (Dependency) test_state.dependencies[typeof (Dependency)];
        }

        static protected Dependency provide_an<Dependency>(Dependency instance) where Dependency : class
        {
            store_a_sut_constructor_argument(instance);
            return instance;
        }

        static void store_a_sut_constructor_argument<ArgumentType>(ArgumentType argument)
        {
            if (! has_no_dependency_for<ArgumentType>()) throw new ArgumentException("A dependency has already been provided for :{0}".format_using(typeof (ArgumentType).proper_name()));
            test_state.dependencies[typeof (ArgumentType)] = argument;
        }

        static protected void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            store_a_sut_constructor_argument(value);
        }

        static bool has_no_dependency_for<Interface>()
        {
            return does_not_have_dependency_registered_for(typeof (Interface));
        }
    }
}