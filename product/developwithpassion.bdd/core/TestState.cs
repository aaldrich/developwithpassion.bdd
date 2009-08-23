using System;
using System.Collections.Generic;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public interface BasicTestState : DependencyBag
    {}

    public interface DependencyBag {
        void store_dependency(Type type, object instance);
        Dependency get_dependency<Dependency>();
        bool has_no_dependency_for<Dependency>();
        void register_dependency_for_sut(Type dependency_type,object instance);
        bool has_no_dependency_for(Type dependency_type);
        object get_the_provided_dependency_assignable_from(Type constructor_parament_type);
        void empty_dependencies();
    }
    public interface TestState<SUT> : BasicTestState
    {
        SUT sut { get; set; }
        Func<SUT> factory { get; set; }
        void run_teardown_pipeline();
        void clear_test_pipeline();
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void reset();
        void run_startup_pipeline();
        void change_because_behaviour_to(Action because_behaviour);
    }

    public class TestStateImplementation<SUT> : TestState<SUT>
    {
        IList<PipelineBehaviour> pipeline_behaviours { get; set; }
        object test { get; set; }
        IDictionary<Type, object> dependencies { get; set; }
        public Action behaviour_performed_in_because { get; set; }

        public SUT sut { get; set; }

        public Func<SUT> factory { get; set; }

        public TestStateImplementation(object test, Func<SUT> factory,IList<PipelineBehaviour> behaviours)
        {
            this.test = test;
            this.factory = factory;

            pipeline_behaviours = this.pipeline_behaviours = behaviours;
            dependencies = new Dictionary<Type, object>();
        }

        public void run_startup_pipeline()
        {
            pipeline_behaviours.each(item => item.start());
        }

        public void change_because_behaviour_to(Action because_behaviour)
        {
            behaviour_performed_in_because = because_behaviour;
        }

        public void run_teardown_pipeline()
        {
            pipeline_behaviours.each(item => item.finish());
        }

        public void clear_test_pipeline()
        {
            pipeline_behaviours.Clear();
        }

        public void store_dependency(Type type, object instance)
        {
            dependencies.Add(type, instance);
        }

        public Dependency get_dependency<Dependency>()
        {
            return (Dependency) dependencies[typeof (Dependency)];
        }

        public bool has_no_dependency_for<Interface>()
        {
            return has_no_dependency_for(typeof (Interface));
        }

        public bool has_no_dependency_for(Type dependency_type)
        {
            return ! dependencies.ContainsKey(dependency_type);
        }

        public void register_dependency_for_sut(Type dependency_type, object instance)
        {
            dependencies[dependency_type] = instance;
        }

        public object get_the_provided_dependency_assignable_from(Type constructor_parament_type)
        {
            return dependencies[constructor_parament_type];
        }

        public void empty_dependencies()
        {
            dependencies.Clear();
        }

        public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            pipeline_behaviours.Add(pipeline_behaviour);
        }

        public void reset()
        {
            behaviour_performed_in_because = null;
        }
    }
}