using System;
using System.Collections.Generic;
using developwithpassion.bdd.concerns;
using developwithpassion.bdd.containers;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.core.extensions;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard;
using MbUnit.Framework;
using Rhino.Mocks;

namespace developwithpassion.bdd.concerns
{
    public interface IObservations {}

    public class PipelinePair
    {
        public PipelinePair(Action context, Action tear_down)
        {
            this.context = context;
            this.tear_down = tear_down;
        }

        public Action context { get; private set; }
        public Action tear_down { get; private set; }
    }

    public abstract class observation_basics
    {
        static protected IList<Action> context_pipeline = new List<Action>();
        static protected IList<Action> teardown_pipeline = new List<Action>();

        static public void add_pipeline_behaviour(PipelinePair pipeline_pair)
        {
            context_pipeline.Add(pipeline_pair.context);
            teardown_pipeline.Add(pipeline_pair.tear_down);
        }
    }

    [Observations]
    public abstract class an_observations_set_of_basic_behaviours<SUT> : observation_basics, IObservations
    {
        static public IDictionary<Type, object> dependencies;
        static public Exception exception_thrown_while_the_sut_performed_its_work;
        static public Action behaviour_performed_in_because;
        static public SUT sut;

        [TestFixtureSetUp]
        public void fixture_setup()
        {
            run_action<before_all_observations>();
        }

        [SetUp]
        public void setup()
        {
            context_pipeline.Clear();
            teardown_pipeline.Clear();

            teardown_pipeline.Add(UnitTestContainer.tear_down);
            behaviour_performed_in_because = null;
            exception_thrown_while_the_sut_performed_its_work = null;
            dependencies = new Dictionary<Type, object>();
            prepare_to_make_an_observation();
        }

        [TearDown]
        public void tear_down()
        {
            run_action<after_each_observation>();
            if (teardown_pipeline.Count > 0) teardown_pipeline.each(x => x());
        }

        [TestFixtureTearDown]
        public void fixture_tear_down()
        {
            run_action<after_all_observations>();
        }


        void prepare_to_make_an_observation()
        {
            run_action<context>();
            if (context_pipeline.Count > 0) context_pipeline.each(x => x());
            sut = create_sut();
            run_action<after_the_sut_has_been_created>();
            run_action<because>();
        }

        after_each_observation a = () => dependencies.Clear();

        public virtual SUT create_sut()
        {
            return default(SUT);
        }

        Command build_command_chain<DelegateType>()
        {
            var actions = new Stack<Command>();
            var current_class = GetType();

            while (current_class.is_a_concern_type())
            {
                actions.Push(new DelegateFieldInvocation(typeof (DelegateType), this, current_class));
                current_class = current_class.BaseType;
            }

            return actions.as_command_chain();
        }

        void run_action<DelegateType>()
        {
            build_command_chain<DelegateType>().run();
        }


        static public void doing(Action because_behaviour)
        {
            behaviour_performed_in_because = because_behaviour;
        }

        static public void doing<T>(Func<IEnumerable<T>> behaviour)
        {
            doing(() => behaviour().force_traversal());
        }

        static public Exception exception_thrown_by_the_sut
        {
            get { return exception_thrown_while_the_sut_performed_its_work ?? (exception_thrown_while_the_sut_performed_its_work = get_exception_throw_by(behaviour_performed_in_because)); }
        }

        static Exception get_exception_throw_by(Action because_behaviour)
        {
            return because_behaviour.get_exception();
        }

        static public InterfaceType container_dependency<InterfaceType>() where InterfaceType : class
        {
            return container_dependency(an<InterfaceType>());
        }

        static public InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class
        {
            UnitTestContainer.add_implementation_of(instance);
            return instance;
        }

        static public object an_item_of(Type type)
        {
            return MockRepository.GenerateStub(type, new object[0]);
        }

        static public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return MockRepository.GenerateStub<InterfaceType>();
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