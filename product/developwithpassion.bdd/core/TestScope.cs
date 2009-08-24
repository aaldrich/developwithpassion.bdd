using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.core.observations;

namespace developwithpassion.bdd.core
{
    public interface TestScope<SUT>
    {
        ChangeValueInPipeline change(Expression<Func<object>> _expression);
        void doing(Action because_behaviour);
        void doing<T>(Func<IEnumerable<T>> behaviour);
        Exception exception_thrown_by_the_sut { get; }
        InterfaceType container_dependency<InterfaceType>() where InterfaceType : class;
        InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class;
        object an_item_of(Type type);
        InterfaceType an<InterfaceType>() where InterfaceType : class;
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void add_pipeline_behaviour(Action context, Action teardown);
    }

    public class TestScopeImplementation<SUT> : TestScope<SUT>
    {
        public Observations<SUT> observation_context;

        public TestScopeImplementation(Observations<SUT> observation_context)
        {
            this.observation_context = observation_context;
        }

        public ChangeValueInPipeline change(Expression<Func<object>> _expression)
        {
            return observation_context.change(_expression);
        }

        public void doing(Action because_behaviour)
        {
            observation_context.doing(because_behaviour);
        }

        public void doing<T>(Func<IEnumerable<T>> behaviour)
        {
            observation_context.doing(behaviour);
        }

        public Exception exception_thrown_by_the_sut
        {
            get { return observation_context.exception_thrown_by_the_sut; }
        }

        public InterfaceType container_dependency<InterfaceType>() where InterfaceType : class
        {
            return observation_context.container_dependency(an<InterfaceType>());
        }

        public InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class
        {
            return observation_context.container_dependency(instance);
        }

        public object an_item_of(Type type)
        {
            return observation_context.an_item_of(type);
        }

        public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return observation_context.an<InterfaceType>();
        }

        public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            observation_context.add_pipeline_behaviour(pipeline_behaviour);
        }

        public void add_pipeline_behaviour(Action context, Action teardown)
        {
            observation_context.add_pipeline_behaviour(context, teardown);
        }
    }
}