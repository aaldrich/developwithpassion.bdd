using System.Data;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mocking;
using developwithpassion.bdd.mocking.rhino;
using developwithpassion.bdddoc.core;
using Rhino.Mocks;

namespace test.developwithpassion.bdd.core
{
    public class SystemUnderTestDependencyBuilderSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<SystemUnderTestDependencyBuilder, SystemUnderTestDependencyBuilderImplementation>
        {
            context c = () =>
            {
                dependency_bag = MockRepository.GenerateStub<DependencyBag>();
                mock_factory = MockRepository.GenerateStub<MockFactory>();
            };

            public override SystemUnderTestDependencyBuilder create_sut()
            {
                return new SystemUnderTestDependencyBuilderImplementation(dependency_bag, mock_factory);
            }

            static protected DependencyBag dependency_bag;
            static protected MockFactory mock_factory;
        }

        [Concern(typeof (SystemUnderTestDependencyBuilderImplementation))]
        public class when_requesting_a_dependency : concern
        {
            because b = () =>
            {
                result = sut.the_dependency<IDbConnection>();
            };

            public class and_the_dependencies_have_not_already_been_provided : when_requesting_a_dependency
            {
                context c = () =>
                {
                    dependency_bag.Stub(x => x.has_no_dependency_for<IDbConnection>()).Return(true);
                    dependency_bag.Stub(x => x.get_dependency<IDbConnection>()).Return(connection);
                };

                it should_return_the_dependency_and_store_it_in_the_dependencies_dictionary = () =>
                {
                    result.should_be_equal_to(connection);
                    dependency_bag.received(x => x.store_dependency(typeof (IDbConnection), connection));
                };
            }

            public class and_the_dependencies_have_been_provided : when_requesting_a_dependency
            {
                context c = () =>
                {
                    dependency_bag.Stub(x => x.has_no_dependency_for<IDbConnection>()).Return(false);
                    dependency_bag.Stub(x => x.get_dependency<IDbConnection>()).Return(connection);
                };

                it should_not_restore_the_dependency_and_return_the_dependency = () =>
                {
                    result.should_be_equal_to(connection);
                    dependency_bag.never_received(x => x.store_dependency(typeof (IDbConnection), connection));
                };
            }

            context c = () =>
            {
                connection = MockRepository.GenerateStub<IDbConnection>();

                mock_factory.Stub(x => x.create_stub<IDbConnection>()).Return(connection);
            };


            static protected IDbConnection result;
            static protected IDbConnection connection;
        }
    }
}