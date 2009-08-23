using System;
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
            static protected IDbConnection connection;
            static protected IDbCommand command;

            context c = () =>
            {
                connection = an<IDbConnection>();
                command = an<IDbCommand>();
                builder = the_dependency<SystemUnderTestDependencyBuilder>();

                builder.Stub(x => x.all_dependencies()).Return(new object[] {command, connection});
            };

            public override SystemUnderTestFactory create_sut()
            {
                return new SystemUnderTestFactoryImplementation(builder);
            }

            static SystemUnderTestDependencyBuilder builder;
        }

        [Concern(typeof (SystemUnderTestFactoryImplementation))]
        public class when_creating_the_system_under_test_and_there_are_dependencies_that_have_not_been_registered : concern
        {
            because b = () =>
            {
                result = sut.create<AClassWithDependencies, AClassWithDependencies>();
            };

            it should_automatically_register_the_dependencies_before_creating_the_sut = () =>
            {
                result.should_not_be_null();
                result.command.should_not_be_null();
                result.connection.should_not_be_null();
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