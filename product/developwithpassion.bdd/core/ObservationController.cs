using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.mbunit.standard.observations;

namespace developwithpassion.bdd.core
{
    public interface ObservationController<Contract, Class, MockFactoryAdapter> : ObservationBasics where MockFactoryAdapter : MockFactory, new()
    {
        void init();
        Observations<Contract> observation_context { get; }
        TestState<Contract> test_state { get; }
        TestScopeImplementation<Contract> test_scope_implementation { get; }
        Exception exception_thrown_by_the_sut { get; }
        Contract sut { get; }
        void reset();
        void tear_down();
        void after_all_observations();
        ChangeValueInPipeline change(Expression<Func<object>> expression);
        void doing(Action behaviour);
        void doing<T>(Func<IEnumerable<T>> behaviour);
    }

    public class ObservationControllerImplementation<Contract, Class, MockFactoryAdapter> : ObservationController<Contract, Class, MockFactoryAdapter> where Class : Contract
                                                                                                                                                       where MockFactoryAdapter : MockFactory, new()
    {
        public Observations<Contract> observation_context { get; set; }
        public TestState<Contract> test_state { get; private set; }
        public TestScopeImplementation<Contract> test_scope_implementation { get; private set; }
        object raw_test_runner;
        Func<Contract> factory;

        public ObservationControllerImplementation(object raw_test_runner, Func<Contract> factory)
        {
            this.raw_test_runner = raw_test_runner;
            this.factory = factory;
        }

        public void init()
        {
            test_state = new TestStateImplementation<Contract>(raw_test_runner, factory, new List<PipelineBehaviour>());
            var args = new ObservationContextArgs<Contract>
                       {
                           mock_factory = new MockFactoryAdapter(),
                           state = test_state,
                           test = raw_test_runner
                       }
                ;
            observation_context = new ObservationContextFactoryImplementation().create_from(args);
            observation_context.before_all_observations();
            test_scope_implementation = new TestScopeImplementation<Contract>(observation_context);
        }

        public void reset()
        {
            observation_context.reset();
        }

        public void tear_down()
        {
            observation_context.tear_down();
        }

        public ChangeValueInPipeline change(Expression<Func<object>> expression)
        {
            return observation_context.change(expression);
        }

        public void doing(Action behaviour)
        {
            observation_context.doing(behaviour);
        }

        public void doing<T>(Func<IEnumerable<T>> behaviour)
        {
            observation_context.doing(behaviour);
        }

        public void after_all_observations()
        {
            observation_context.after_all_observations();
        }

        public Exception exception_thrown_by_the_sut
        {
            get { return observation_context.exception_thrown_by_the_sut; }
        }

        public Dependency the_dependency<Dependency>() where Dependency : class
        {
            return observation_context.the_dependency<Dependency>();
        }

        public void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            observation_context.provide_a_basic_sut_constructor_argument(value);
        }

        public InterfaceType container_dependency<InterfaceType>() where InterfaceType : class
        {
            return observation_context.container_dependency<InterfaceType>();
        }

        public InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class
        {
            return observation_context.container_dependency<InterfaceType>(instance);
        }

        public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return observation_context.an<InterfaceType>();
        }

        public object an_item_of(Type type)
        {
            return observation_context.an_item_of(type);
        }

        public Contract1 build_sut<Contract1, Class1>()
        {
            return observation_context.build_sut<Contract1, Class1>();
        }

        public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            observation_context.add_pipeline_behaviour(pipeline_behaviour);
        }

        public void add_pipeline_behaviour(Action context, Action teardown)
        {
            observation_context.add_pipeline_behaviour(context, teardown);
        }

        public Contract sut
        {
            get { return test_state.sut;}
        }
    }
}