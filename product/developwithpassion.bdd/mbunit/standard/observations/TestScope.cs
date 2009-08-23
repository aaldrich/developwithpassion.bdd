using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.core;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    public interface TestScope<SUT> {
        ChangeValueInPipeline change(Expression<Func<object>> _expression);
        void doing(Action because_behaviour);
        void doing<T>(Func<IEnumerable<T>> behaviour);
        Exception exception_thrown_by_the_sut { get; }
        SUT sut { get; set; }
        InterfaceType container_dependency<InterfaceType>() where InterfaceType : class;
        InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class;
        object an_item_of(Type type);
        InterfaceType an<InterfaceType>() where InterfaceType : class;
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void add_pipeline_behaviour(Action context, Action teardown);
        SUT create_sut();
    }
}