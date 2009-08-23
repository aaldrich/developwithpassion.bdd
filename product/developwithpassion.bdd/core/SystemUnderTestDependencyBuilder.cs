using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public interface SystemUnderTestDependencyBuilder {
        Dependency the_dependency<Dependency>() where Dependency : class;
        void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value);
        object[] all_dependencies(IEnumerable<Type> enumerable);
        void register_only_if_missing(Type dependency_type);
    }
    public class SystemUnderTestDependencyBuilderImplementation : SystemUnderTestDependencyBuilder
    {
        DependencyBag dependency_bag;
        MockFactory mock_factory;

        public SystemUnderTestDependencyBuilderImplementation(DependencyBag dependency_bag, MockFactory mock_factory)
        {
            this.dependency_bag = dependency_bag;
            this.mock_factory = mock_factory;
        }

        bool dependency_needs_to_be_registered_for(Type dependency_type)
        {
            return dependency_bag.has_no_dependency_for(dependency_type) &&
                   is_a_depedency_that_can_automatically_be_created(dependency_type);
        }

        bool is_a_depedency_that_can_automatically_be_created(Type dependency_type)
        {
            return ! dependency_type.IsValueType;
        }

        public void register_only_if_missing(Type dependency_type)
        {
            if (dependency_needs_to_be_registered_for(dependency_type)) dependency_bag.register_dependency_for_sut(dependency_type,mock_factory.create_stub(dependency_type));
        }

        public Dependency the_dependency<Dependency>() where Dependency : class
        {
            if (dependency_bag.has_no_dependency_for<Dependency>())
                dependency_bag.store_dependency(typeof (Dependency), mock_factory.create_stub<Dependency>());
            return dependency_bag.get_dependency<Dependency>();
        }

        void store_a_sut_constructor_argument<ArgumentType>(ArgumentType argument)
        {
            ensure_the_dependency_has_not_already_been_register<ArgumentType>();
            dependency_bag.store_dependency(typeof (ArgumentType), argument);
        }

        void ensure_the_dependency_has_not_already_been_register<ArgumentType>()
        {
            if (! dependency_bag.has_no_dependency_for<ArgumentType>())
                throw new ArgumentException(
                    "A dependency has already been provided for :{0}".format_using(typeof (ArgumentType).proper_name()));
        }

        public void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            store_a_sut_constructor_argument(value);
        }

        public object[] all_dependencies(IEnumerable<Type> constructor_parameter_types)
        {
            return
                constructor_parameter_types.Select(parameter => dependency_bag.get_the_provided_dependency_assignable_from(parameter)).
                    ToArray();
        }
    }
}