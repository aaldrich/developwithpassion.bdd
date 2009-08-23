using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.core;
using MbUnit.Framework;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    [Observations]
    public abstract class sut_observation_context<Contract, Class, MockFactoryAdapter> : Observations where Class : Contract
                                                                                                      where MockFactoryAdapter : MockFactory, new()
    {
        static public Observations<Contract> observation_context;
        static public TestState<Contract> test_state;
        static MockFactory mock_factory;
        static TestScopeImplementation<Contract> test_scope_implementation;

        [TestFixtureSetUp]
        public void fixture_setup()
        {
            mock_factory = new MockFactoryAdapter();
            test_state = new TestStateImplementation<Contract>(this, create_sut);
            var dependency_builder = new SystemUnderTestDependencyBuilderImplementation(test_state, mock_factory);
            observation_context = new ObservationContext<Contract>(test_state,
                                                                   new ObservationCommandFactoryImplementation<Contract>(test_state,
                                                                                                                         new DelegateControllerImplementation
                                                                                                                             (this)),
                                                                   mock_factory,
                                                                   dependency_builder,
                                                                   new SystemUnderTestFactoryImplementation(dependency_builder));
            observation_context.before_all_observations();

            test_scope_implementation = new TestScopeImplementation<Contract>(observation_context, test_state);
        }

        static public TestScopeImplementation<Contract> context
        {
            get { return test_scope_implementation; }
        }

        [SetUp]
        public void setup()
        {
            observation_context.reset();
        }

        [TearDown]
        public void tear_down()
        {
            observation_context.tear_down();
        }

        [TestFixtureTearDown]
        public void fixture_tear_down()
        {
            observation_context.after_all_observations();
        }


        [Obsolete("use context property to access testing dsl")]
        static public ChangeValueInPipeline change(Expression<Func<object>> static_expression)
        {
            return observation_context.change(static_expression);
        }

        [Obsolete("use context property to access testing dsl")]
        static public void doing(Action because_behaviour)
        {
            observation_context.doing(because_behaviour);
        }

        [Obsolete("use context property to access testing dsl")]
        static public void doing<T>(Func<IEnumerable<T>> behaviour)
        {
            observation_context.doing(behaviour);
        }

        [Obsolete("use context property to access testing dsl")]
        static public Exception exception_thrown_by_the_sut
        {
            get { return observation_context.exception_thrown_by_the_sut; }
        }

        [Obsolete("use context property to access testing dsl")]
        static public InterfaceType container_dependency<InterfaceType>() where InterfaceType : class
        {
            return observation_context.container_dependency(an<InterfaceType>());
        }

        [Obsolete("use context property to access testing dsl")]
        static public InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class
        {
            return observation_context.container_dependency(instance);
        }

        [Obsolete("use context property to access testing dsl")]
        static public object an_item_of(Type type)
        {
            return observation_context.an_item_of(type);
        }

        [Obsolete("use context property to access testing dsl")]
        static public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return observation_context.an<InterfaceType>();
        }

        [Obsolete("use context property to access testing dsl")]
        static public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            observation_context.add_pipeline_behaviour(pipeline_behaviour);
        }

        [Obsolete("use context property to access testing dsl")]
        static public void add_pipeline_behaviour(Action context, Action teardown)
        {
            observation_context.add_pipeline_behaviour(context, teardown);
        }

        [Obsolete("use context property to access testing dsl")]
        static public Contract sut
        {
            get { return test_state.sut; }
            set { test_state.sut = value; }
        }

        [Obsolete("use context property to access testing dsl")]
        public virtual Contract create_sut()
        {
            return observation_context.build_sut<Contract, Class>();
        }
    }
}