using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.containers;
using System.Linq;
using developwithpassion.bdd.core.extensions;
using developwithpassion.bdd.mocking;

namespace developwithpassion.bdd.core.observations
{
    public interface ObservationBasics {
        Dependency the_dependency<Dependency>() where Dependency : class;
        void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value);
        InterfaceType container_dependency<InterfaceType>() where InterfaceType : class;
        InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class;
        InterfaceType an<InterfaceType>() where InterfaceType : class;
        object an_item_of(Type type);
        Contract build_sut<Contract, Class>();
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void add_pipeline_behaviour(Action context, Action teardown);
    }

    public interface Observations<SUT>  : ObservationBasics{
        void tear_down();
        void reset();
        void doing(Action because_behaviour);
        void doing<T>(Func<IEnumerable<T>> behaviour);
        ChangeValueInPipeline change(Expression<Func<object>> static_expression);
        void before_all_observations();
        void after_all_observations();
        Exception exception_thrown_by_the_sut { get;}
        Action because_behaviour { get; }
    }

    public class ObservationContext<SUT> : Observations<SUT>
    {
        TestState<SUT> test_state { get; set; }
        ObservationCommandFactory observation_command_factory;
        MockFactory mock_factory;
        SystemUnderTestDependencyBuilder system_under_test_dependency_builder { get; set; }
        SystemUnderTestFactory system_under_test_factory { get; set; }
        Exception exception_thrown;
        public Action because_behaviour { get; private set; }


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
            this.because_behaviour = because_behaviour;
        }

        public void doing<T>(Func<IEnumerable<T>> behaviour)
        {
            doing(() => behaviour().Count());
        }


        public Exception exception_thrown_by_the_sut
        {
            get
            {
                return exception_thrown ??
                       (exception_thrown=
                        get_exception_throw_by(because_behaviour));
            }
        }

        Exception get_exception_throw_by(Action action)
        {
            return action.get_exception();
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