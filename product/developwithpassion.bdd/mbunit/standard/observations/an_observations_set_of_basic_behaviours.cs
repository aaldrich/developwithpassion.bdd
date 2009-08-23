using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.core;
using MbUnit.Framework;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    public interface IObservations {}

    [Observations]
    public abstract class an_observations_set_of_basic_behaviours<SUT> : IObservations
    {
        static public Observations<SUT> observation_context;
        static public TestState<SUT> test_state;
        static MockFactory mock_factory;

        [TestFixtureSetUp]
        public void fixture_setup()
        {
            mock_factory = new RhinoMocksMockFactory();
            test_state = new TestStateImplementation<SUT>(this, create_sut);
            var dependency_builder = new SystemUnderTestDependencyBuilderImplementation(
                test_state, mock_factory);
            observation_context = new ObservationContext<SUT>(test_state,
                                                              new ObservationCommandFactoryImplementation<SUT>(test_state,
                                                                                                               new DelegateControllerImplementation
                                                                                                                   (this)),
                                                              new RhinoMocksMockFactory(),
                                                              dependency_builder,
                                                              new SystemUnderTestFactoryImplementation(dependency_builder));
            observation_context.before_all_observations();
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


        static public ChangeValueInPipeline change(Expression<Func<object>> static_expression)
        {
            return observation_context.change(static_expression);
        }

        static public void doing(Action because_behaviour)
        {
            observation_context.doing(because_behaviour);
        }

        static public void doing<T>(Func<IEnumerable<T>> behaviour)
        {
            observation_context.doing(behaviour);
        }

        static public Exception exception_thrown_by_the_sut
        {
            get { return observation_context.exception_thrown_by_the_sut; }
        }

        static public InterfaceType container_dependency<InterfaceType>() where InterfaceType : class
        {
            return observation_context.container_dependency(an<InterfaceType>());
        }

        static public InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class
        {
            return observation_context.container_dependency(instance);
        }

        static public object an_item_of(Type type)
        {
            return observation_context.an_item_of(type);
        }

        static public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return observation_context.an<InterfaceType>();
        }

        static public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            observation_context.add_pipeline_behaviour(pipeline_behaviour);
        }

        static public void add_pipeline_behaviour(Action context, Action teardown)
        {
            observation_context.add_pipeline_behaviour(context, teardown);
        }

        static public SUT sut
        {
            get { return test_state.sut; }
            set { test_state.sut = value; }
        }

        public virtual SUT create_sut()
        {
            return default(SUT);
        }
    }
}