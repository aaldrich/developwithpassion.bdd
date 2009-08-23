using System;
using System.Collections.Generic;

namespace developwithpassion.bdd.core
{
    public interface DependencyBag {
        IDictionary<Type, object> dependencies { get; set; }
        void store_dependency(Type type, object instance);
        Dependency get_dependency<Dependency>();
        bool has_no_dependency_for<Dependency>();
        void register_dependency_for_sut(Type dependency_type,object instance);
        bool has_no_dependency_for(Type dependency_type);
        object get_the_provided_dependency_assignable_from(Type constructor_parament_type);
        void empty_dependencies();
    }
}