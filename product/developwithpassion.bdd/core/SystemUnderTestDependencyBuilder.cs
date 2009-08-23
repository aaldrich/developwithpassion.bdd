using System;
using System.Collections.Generic;

namespace developwithpassion.bdd.core
{
    public interface SystemUnderTestDependencyBuilder {
        Dependency the_dependency<Dependency>() where Dependency : class;
        void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value);
        object[] all_dependencies(IEnumerable<Type> enumerable);
        void register_only_if_missing(Type dependency_type);
    }
}