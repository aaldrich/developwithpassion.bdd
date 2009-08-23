using System.Data;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using Rhino.Mocks;

namespace test.developwithpassion.bdd
{
    public class SystemUnderTestFactorySpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<SystemUnderTestFactory,
                                            SystemUnderTestFactoryImplementation>
        {
            context c = () =>
            {
                connection = MockRepository.GenerateStub<IDbConnection>();
                command = MockRepository.GenerateStub<IDbCommand>();
                builder = MockRepository.GenerateStub<SystemUnderTestDependencyBuilder>();

                builder.Stub(x => x.all_dependencies()).Return(new object[] {command, connection});
            };

            public override SystemUnderTestFactory create_sut()
            {
                return new SystemUnderTestFactoryImplementation(builder);
            }

            static protected IDbConnection connection;
            static protected IDbCommand command;
            static SystemUnderTestDependencyBuilder builder;
        }

        [Concern(typeof (SystemUnderTestFactoryImplementation))]
        public class when_creating_the_system_under_test : concern
        {
            because b = () =>
            {
                result = context.sut.create<AClassWithDependencies, AClassWithDependencies>();
            };


            it should_create_an_instance_of_the_system_under_test_using_the_builders_constructor_arg_array = () =>
            {
                result.should_not_be_null();
                result.command.should_be_equal_to(command);
                result.connection.should_be_equal_to(connection);
            };

            static AClassWithDependencies result;
            static SystemUnderTestDependencyBuilder builder;
        }

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