using System;
using System.Collections.Generic;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public interface DependencyBag {
        void store_dependency(Type type, object instance);
        Dependency get_dependency<Dependency>();
        bool has_no_dependency_for<Dependency>();
        void register_dependency_for_sut(Type dependency_type,object instance);
        bool has_no_dependency_for(Type dependency_type);
        object get_the_provided_dependency_assignable_from(Type constructor_parament_type);
        void empty_dependencies();
    }
    public interface TestState<SUT> : DependencyBag
    {
        SUT sut { get; set; }
        void run_teardown_pipeline();
        void clear_test_pipeline();
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void run_startup_pipeline();
        void build_sut();
    }

    public class TestStateImplementation<SUT> : TestState<SUT>
    {
        IList<PipelineBehaviour> pipeline_behaviours { get; set; }
        object test { get; set; }
        IDictionary<Type, object> dependencies { get; set; }
        Func<SUT> factory { get; set; }
        public SUT sut { get; set; }

        public TestStateImplementation(object test, Func<SUT> factory,IList<PipelineBehaviour> behaviours)
        {
            this.test = test;
            this.factory = factory;

            pipeline_behaviours = this.pipeline_behaviours = behaviours;
            dependencies = new Dictionary<Type, object>();
        }

        public void build_sut()
        {
            sut = factory();
        }

        public SUT create_sut()
        {
            return factory();
        }

        public void run_startup_pipeline()
        {
            pipeline_behaviours.each(item => item.start());
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

    }
}