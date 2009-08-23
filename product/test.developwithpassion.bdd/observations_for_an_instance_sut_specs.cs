using System.Data;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using MbUnit.Framework;
using Rhino.Mocks;

namespace test.developwithpassion.bdd
{
    public class observations_for_an_instance_sut_specs
    {
        [Observations]
        public abstract class concern
        {
            protected SampleTestWithAnSut sut;
            protected Observations<AClassWithDependencies> observations;
            protected ObservationCommandFactory command_factory;
            protected TestStateImplementation<AClassWithDependencies> state;

            [SetUp]
            public void setup()
            {
                sut = new SampleTestWithAnSut();

                command_factory = MockRepository.GenerateStub<ObservationCommandFactory>();
                state = new TestStateImplementation<AClassWithDependencies>(sut, sut.create_sut);
                observations = new ObservationContext<AClassWithDependencies>(
                    state, command_factory, new RhinoMocksMockFactory());
                an_observations_set_of_basic_behaviours<AClassWithDependencies>.observation_context = observations;
                an_observations_set_of_basic_behaviours<AClassWithDependencies>.test_state = state;
                establish_context();
                because();
            }

            protected virtual void establish_context() {}
            protected abstract void because();
        }

        [Concern(typeof (observations_for_an_instance_sut<,>))]
        public class when_creating_an_instance_of_the_sut_and_no_dependencies_have_been_provided : concern
        {
            AClassWithDependencies result;

            protected override void because()
            {
                result = sut.create_sut();
            }


            [Observation]
            public void should_create_the_sut_and_automatically_mock_out_dependencies_that_can_be_mocked()
            {
                result.should_not_be_null();
                result.connection.should_not_be_null();
                result.command.should_not_be_null();
            }
        }

        [Concern(typeof (observations_for_an_instance_sut<,>))]
        public class when_creating_an_instance_of_the_sut_and_dependencies_have_been_provided : concern
        {
            AClassWithDependencies result;
            IDbConnection connection;
            IDbCommand command;

            protected override void establish_context()
            {
                an_observations_set_of_basic_behaviours<AClassWithDependencies>.test_state.dependencies.Clear();
                connection = MockRepository.GenerateMock<IDbConnection>();
                command = MockRepository.GenerateMock<IDbCommand>();
                SampleTestWithAnSut.test_state.dependencies.Add(typeof (IDbConnection), connection);
                SampleTestWithAnSut.test_state.dependencies.Add(typeof (IDbCommand), command);
            }

            protected override void because()
            {
                result = sut.create_sut();
            }


            [Observation]
            public void should_create_the_sut_using_the_dependencies_that_were_provided_by_the_client()
            {
                result.should_not_be_null();
                result.connection.should_be_equal_to(connection);
                result.command.should_be_equal_to(command);
            }
        }

        public class SampleTestWithAnSut : observations_for_an_instance_sut<AClassWithDependencies, AClassWithDependencies> {}


        public class AClassWithDependencies
        {
            public IDbCommand command { get; private set; }
            public IDbConnection connection { get; private set; }

            public AClassWithDependencies(IDbCommand command, IDbConnection connection)
            {
                this.command = command;
                this.connection = connection;
            }

            public void open_the_connection()
            {
                connection.Open();
            }

            public void update_the_commands_transaction(IDbTransaction transaction)
            {
                command.Transaction = transaction;
            }
        }
    }
}