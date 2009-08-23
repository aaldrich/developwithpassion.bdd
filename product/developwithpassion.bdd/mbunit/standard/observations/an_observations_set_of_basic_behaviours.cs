using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.concerns;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit.standard;
using developwithpassion.bdd.mbunit.standard.observations;
using MbUnit.Framework;

namespace developwithpassion.bdd.concerns
{
    public interface IObservations {}

    [Observations]
    public abstract class an_observations_set_of_basic_behaviours<SUT> : observation_basics, IObservations
    {
        public static Observations<SUT> observation_context;

        [TestFixtureSetUp]
        public void fixture_setup()
        {
            observation_context = new ObservationContext<SUT>(this, context_pipeline);
            observation_context.run_action<before_all_observations>();
            observation_context.factory = create_sut;
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
            observation_context.run_action<after_all_observations>();
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

        static public IDictionary<Type, object> dependencies
        {
            get { return observation_context.dependencies; }
            set { observation_context.dependencies = value; }
        }

        static public Action behaviour_performed_in_because
        {
            get { return observation_context.behaviour_performed_in_because; }
        }

        static public SUT sut
        {
            get { return observation_context.sut; }
            set { observation_context.sut = value; }
        }

        public virtual SUT create_sut()
        {
            return default(SUT);
        }
    }
}

static public class ConcernExtensions
{
    static public bool is_a_concern_type(this Type type)
    {
        return typeof (IObservations)
            .IsAssignableFrom(type);
    }
}