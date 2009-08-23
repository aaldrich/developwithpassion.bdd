using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.containers;
using developwithpassion.bdd.mbunit;

namespace developwithpassion.bdd.core
{
    public interface Observations<SUT> {
        void tear_down();
        void reset();
        void doing(Action because_behaviour);
        void doing<T>(Func<IEnumerable<T>> behaviour);
        InterfaceType container_dependency<InterfaceType>() where InterfaceType : class;
        InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class;
        object an_item_of(Type type);
        InterfaceType an<InterfaceType>() where InterfaceType : class;
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void add_pipeline_behaviour(Action context, Action teardown);
        ChangeValueInPipeline change(Expression<Func<object>> static_expression);
        void before_all_observations();
        void after_all_observations();
        Exception exception_thrown_by_the_sut { get;}
        Contract build_sut<Contract, Class>();
        Dependency the_dependency<Dependency>() where Dependency : class;
        void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value);
    }

    public class ObservationContext<SUT> : Observations<SUT>
    {
        TestState<SUT> test_state { get; set; }
        ObservationCommandFactory observation_command_factory;
        MockFactory mock_factory;
        SystemUnderTestDependencyBuilder system_under_test_dependency_builder { get; set; }
        SystemUnderTestFactory system_under_test_factory { get; set; }
        Exception exception_thrown;


        public ObservationContext(TestState<SUT> test_state_implementation, ObservationCommandFactory observation_command_factory, MockFactory mock_factory,
                                  SystemUnderTestDependencyBuilder system_under_test_dependency_builder,
                                  SystemUnderTestFactory system_under_test_factory)
        {
            test_state = test_state_implementation;
            this.observation_command_factory = observation_command_factory;
            this.mock_factory = mock_factory;
            this.system_under_test_dependency_builder = system_under_test_dependency_builder;
            this.system_under_test_factory = system_under_test_factory;
        }

        public Contract build_sut<Contract, Class>()
        {
            return system_under_test_factory.create<Contract, Class>();
        }

        public void tear_down()
        {
            observation_command_factory.create_teardown_command().run();
        }

        public void reset()
        {
            observation_command_factory.create_reset_command().run();
            observation_command_factory.create_prepare_observations_command().run();
        }

        public void doing(Action because_behaviour)
        {
            test_state.behaviour_performed_in_because = because_behaviour;
        }

        public void doing<T>(Func<IEnumerable<T>> behaviour)
        {
            doing(() => behaviour().force_traversal());
        }


        public Exception exception_thrown_by_the_sut
        {
            get
            {
                return exception_thrown ??
                       (exception_thrown=
                        get_exception_throw_by(test_state.behaviour_performed_in_because));
            }
        }

        Exception get_exception_throw_by(Action because_behaviour)
        {
            return because_behaviour.get_exception();
        }

        public InterfaceType container_dependency<InterfaceType>() where InterfaceType : class
        {
            return container_dependency(an<InterfaceType>());
        }

        public InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class
        {
            UnitTestContainer.add_implementation_of(instance);
            return instance;
        }

        public object an_item_of(Type type)
        {
            return mock_factory.create_stub(type);
        }

        public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return mock_factory.create_stub<InterfaceType>();
        }

        public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            test_state.add_pipeline_behaviour(pipeline_behaviour);
        }

        public void add_pipeline_behaviour(Action context, Action teardown)
        {
            add_pipeline_behaviour(new PipelineBehaviour(context, teardown));
        }

        public ChangeValueInPipeline change(Expression<Func<object>> static_expression)
        {
            return new ChangeValueInPipeline(add_pipeline_behaviour, static_expression);
        }

        public void before_all_observations()
        {
            observation_command_factory.create_before_all_observations_command().run();
        }

        public void after_all_observations()
        {
            observation_command_factory.create_after_all_observations_command().run();
        }

        public Dependency the_dependency<Dependency>() where Dependency : class
        {
            return system_under_test_dependency_builder.the_dependency<Dependency>();
        }

        public void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            system_under_test_dependency_builder.provide_a_basic_sut_constructor_argument(value);
        }
    }
}