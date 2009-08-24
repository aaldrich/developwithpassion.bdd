using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.core;
using developwithpassion.bdd.core.observations;
using developwithpassion.bdd.mocking;
using MbUnit.Framework;

namespace developwithpassion.bdd.harnesses.mbunit
{
    [Observations]
    public abstract class sut_observation_context<Contract, Class, MockFactoryAdapter> : Context where Class : Contract
                                                                                                 where MockFactoryAdapter : MockFactory, new()
    {
        static public ObservationController<Contract,Class,MockFactoryAdapter> observation_controller;

        [TestFixtureSetUp]
        public void fixture_setup()
        {
            observation_controller = new ObservationControllerImplementation<Contract, Class, MockFactoryAdapter>(this,create_sut);
            observation_controller.init();
        }

        static public TestScopeImplementation<Contract> context
        {
            get { return observation_controller.test_scope_implementation; }
        }

        [SetUp]
        public void setup()
        {
            observation_controller.reset();
        }

        [TearDown]
        public void tear_down()
        {
            observation_controller.tear_down();
        }

        [TestFixtureTearDown]
        public void fixture_tear_down()
        {
            observation_controller.after_all_observations();
        }


        [Obsolete("use context property to access testing dsl")]
        static public ChangeValueInPipeline change(Expression<Func<object>> static_expression)
        {
            return observation_controller.change(static_expression);
        }

        [Obsolete("use context property to access testing dsl")]
        static public void doing(Action because_behaviour)
        {
            observation_controller.doing(because_behaviour);
        }

        [Obsolete("use context property to access testing dsl")]
        static public void doing<T>(Func<IEnumerable<T>> behaviour)
        {
            observation_controller.doing(behaviour);
        }

        [Obsolete("use context property to access testing dsl")]
        static public Exception exception_thrown_by_the_sut
        {
            get { return observation_controller.exception_thrown_by_the_sut; }
        }

        [Obsolete("use context property to access testing dsl")]
        static public InterfaceType container_dependency<InterfaceType>() where InterfaceType : class
        {
            return observation_controller.container_dependency(an<InterfaceType>());
        }

        [Obsolete("use context property to access testing dsl")]
        static public InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class
        {
            return observation_controller.container_dependency(instance);
        }

        [Obsolete("use context property to access testing dsl")]
        static public object an_item_of(Type type)
        {
            return observation_controller.an_item_of(type);
        }

        [Obsolete("use context property to access testing dsl")]
        static public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return observation_controller.an<InterfaceType>();
        }

        [Obsolete("use context property to access testing dsl")]
        static public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            observation_controller.add_pipeline_behaviour(pipeline_behaviour);
        }

        [Obsolete("use context property to access testing dsl")]
        static public void add_pipeline_behaviour(Action context, Action teardown)
        {
            observation_controller.add_pipeline_behaviour(context, teardown);
        }

        [Obsolete("use context property to access testing dsl")]
        static public Contract sut
        {
            get { return observation_controller.sut; }
        }

        [Obsolete("use context property to access testing dsl")]
        public virtual Contract create_sut()
        {
            return observation_controller.build_sut<Contract, Class>();
        }
    }
}