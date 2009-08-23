using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mocking.rhino;
using developwithpassion.bdddoc.core;
using developwithpassion.commons.core.infrastructure.containers;
using MbUnit.Framework;
using Rhino.Mocks;

namespace test.developwithpassion.bdd.mbunit
{
    public class ObservationContextSpecs
    {
        [Observations]
        public abstract class concern
        {
            protected SampleSetOfObservations test_driver;
            protected Observations<IDbConnection> sut;
            protected DelegateController delegate_controller;
            protected Func<IDbConnection> factory;
            protected MockFactory mock_factory;
            protected ObservationCommandFactory observation_command_factory;
            protected TestState<IDbConnection> test_state;
            SystemUnderTestDependencyBuilder dependency_builder;
            SystemUnderTestFactory sut_factory;


            [SetUp]
            public void setup()
            {
                factory = () => new SqlConnection();
                test_driver = new SampleSetOfObservations();
                observation_command_factory = MockRepository.GenerateStub<ObservationCommandFactory>();
                sut_factory = MockRepository.GenerateStub<SystemUnderTestFactory>();
                dependency_builder = MockRepository.GenerateStub<SystemUnderTestDependencyBuilder>();
                delegate_controller = MockRepository.GenerateStub<DelegateController>();
                mock_factory = MockRepository.GenerateStub<MockFactory>();
                test_state = new TestStateImplementation<IDbConnection>(test_driver, factory);

                sut = create_the_sut();
                establish_context();
                because();
            }

            ObservationContext<IDbConnection> create_the_sut()
            {
                return new ObservationContext<IDbConnection>(test_state, observation_command_factory, mock_factory,dependency_builder,sut_factory);
            }

            protected virtual void establish_context() {}
            protected abstract void because();
        }

        [Observations]
        public class when_adding_a_pipeline_action_to_the_context_pipeline : concern
        {
            PipelineBehaviour behaviour;

            protected override void establish_context()
            {
                behaviour = new PipelineBehaviour(() => {}, () => {});
            }

            protected override void because()
            {
                sut.add_pipeline_behaviour(behaviour);
            }

            [Observation]
            public void should_add_the_behaviour_to_the_pipeline_list()
            {
                test_state.pipeline_behaviours.should_contain(behaviour);
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_told_to_reset : concern
        {
            Command reset_command;
            Command prepare_to_make_observations_command;

            protected override void establish_context()
            {
                prepare_to_make_observations_command = MockRepository.GenerateStub<Command>();
                reset_command=  MockRepository.GenerateStub<Command>();
                observation_command_factory.Stub(x => x.create_reset_command()).Return(reset_command);
                observation_command_factory.Stub(x => x.create_prepare_observations_command()).Return(prepare_to_make_observations_command);

            }

            protected override void because()
            {
                sut.reset();
            }


            [Observation]
            public void should_run_the_reset_and_prepare_to_make_observations_command()
            {
                reset_command.received(x => x.run());
                prepare_to_make_observations_command.received(x => x.run());
            }


        }


        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_told_to_teardown : concern
        {
            Command teardown_command;

            protected override void establish_context()
            {
                teardown_command = MockRepository.GenerateStub<Command>();
                observation_command_factory.Stub(x => x.create_teardown_command()).Return(teardown_command);
            }

            protected override void because()
            {
                sut.tear_down();
            }

            [Observation]
            public void should_run_the_teardown_command()
            {
                teardown_command.received(x => x.run());
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_it_is_asked_for_the_exception_that_was_thrown : concern
        {
            static Exception exception = new Exception();
            static bool alternate_because_block_ran;
            Exception result;

            static Action action = () =>
            {
                alternate_because_block_ran = true;
                throw exception;
            };

            protected override void establish_context()
            {
                sut.doing(action);
            }

            protected override void because()
            {
                result = sut.exception_thrown_by_the_sut;
            }

            [Observation]
            public void should_run_the_code_in_the_because_action()
            {
                alternate_because_block_ran.should_be_true();
            }

            [Observation]
            public void should_return_the_exception_throw_in_the_alternate_because_block()
            {
                result.should_be_equal_to(exception);
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class
            when_it_is_asked_for_the_exception_that_was_thrown_and_the_method_it_is_targeting_is_a_method_that_leverages_the_yield_keyword :
                concern
        {
            Exception exception = new Exception();
            bool alternate_because_block_ran;
            Exception result;


            protected override void establish_context()
            {
                sut.doing(() => new SampleClassWithYieldingMethodThrowingAnException().get_numbers());
            }

            protected override void because()
            {
                result = sut.exception_thrown_by_the_sut;
            }


            [Observation]
            public void should_return_the_exception_throw_in_the_alternate_because_block()
            {
                result.should_be_an_instance_of<ArgumentException>();
            }
        }

        public class SampleClassWithYieldingMethodThrowingAnException
        {
            public IEnumerable<int> get_numbers()
            {
                throw new ArgumentException();
                yield return 1;
                yield return 2;
            }
        }

        public class SampleSetOfObservations : an_observations_set_of_basic_behaviours<IDbConnection>
        {
            public override IDbConnection create_sut()
            {
                return an<IDbConnection>();
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_its_doing_method_is_leveraged : concern
        {
            static Action action = () => {};

            protected override void because()
            {
                sut.doing(action);
            }

            [Observation]
            public void should_store_the_action_as_the_because_action()
            {
                test_state.behaviour_performed_in_because.should_be_equal_to(action);
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_creating_a_mock : concern
        {
            IDbConnection result;
            object result2;
            IDbConnection connection;

            protected override void establish_context()
            {
                connection = MockRepository.GenerateStub<IDbConnection>();
                mock_factory.Stub(x => x.create_stub<IDbConnection>()).Return(connection);
                mock_factory.Stub(x => x.create_stub(typeof (IDbConnection))).Return(connection);
            }

            protected override void because()
            {
                result = sut.an<IDbConnection>();
                result2 = sut.an_item_of(typeof (IDbConnection));
            }

            [Observation]
            public void should_return_the_mocks_created_by_the_mock_factory()
            {
                result.should_be_equal_to(connection);
                result2.should_be_equal_to(connection);
            }
        }
    }

    public abstract class concern_that_has_a_provided_value_type_constructor_argument_that_can_be_overriden_in_derived_types
    {
        context c = () =>
        {
            number_to_change = 23;
        };

        static int number_to_change;
    }

    [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
    public class when_it_makes_use_of_a_container_dependency :
        observations_for_a_sut_without_a_contract<SomeObjectWithContainerDependencies>
    {
        static IDbConnection connection;

        context c = () =>
        {
            connection = container_dependency<IDbConnection>();
        };

        because b = () =>
        {
            sut.do_something();
        };

        [Observation]
        public void the_sut_should_be_able_to_access_the_item_from_the_container()
        {
            connection.was_told_to(x => x.Open());
        }
    }

    public class SomeObjectWithContainerDependencies
    {
        public void do_something()
        {
            IOC.get().instance_of<IDbConnection>().Open();
        }
    }
}