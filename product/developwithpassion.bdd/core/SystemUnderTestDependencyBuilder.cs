using System;

namespace developwithpassion.bdd.core
{
    public interface SystemUnderTestDependencyBuilder {
        bool dependency_needs_to_be_registered_for(Type dependency_type);
        bool is_a_depedency_that_can_automatically_be_created(Type dependency_type);
        bool does_not_have_dependency_registered_for(Type dependency_type);
        void register_dependency_for_sut(Type dependency_type);
        Dependency the_dependency<Dependency>() where Dependency : class;
        Dependency provide_an<Dependency>(Dependency instance) where Dependency : class;
        void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value);
        object[] all_dependencies();
        bool has_no_dependency_for<Interface>();
        object get_the_provided_dependency_assignable_from(Type constructor_parament_type);
    }
}