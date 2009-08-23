using System.Data;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using MbUnit.Framework;
using Rhino.Mocks;

namespace test.developwithpassion.bdd.mbunit
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
            protected SystemUnderTestDependencyBuilder dependency_builder;
            protected SystemUnderTestFactory sut_factory;
            protected IDbConnection connection;
            protected IDbCommand command;

            [SetUp]
            public void setup()
            {
                sut = new SampleTestWithAnSut();

                connection = MockRepository.GenerateStub<IDbConnection>();
                command = MockRepository.GenerateStub<IDbCommand>();
                command_factory = MockRepository.GenerateStub<ObservationCommandFactory>();
                sut_factory = MockRepository.GenerateStub<SystemUnderTestFactory>();
                dependency_builder = MockRepository.GenerateStub<SystemUnderTestDependencyBuilder>();
                state = new TestStateImplementation<AClassWithDependencies>(sut, sut.create_sut);
                observations = new ObservationContext<AClassWithDependencies>(
                    state, command_factory, new RhinoMocksMockFactory(),
                    dependency_builder, sut_factory);
                an_observations_set_of_basic_behaviours<AClassWithDependencies>.observation_context = observations;
                an_observations_set_of_basic_behaviours<AClassWithDependencies>.test_state = state;
                establish_context();
                because();
            }

            protected virtual void establish_context() {}
            protected abstract void because();
        }

        [Concern(typeof (observations_for_an_instance_sut<,>))]
        public class when_creating_the_instance_of_the_sut : concern
        {
            AClassWithDependencies result;
            AClassWithDependencies the_sut;

            protected override void establish_context()
            {
                the_sut = MockRepository.GenerateStub<AClassWithDependencies>();
                sut_factory.Stub(x => x.create<AClassWithDependencies, AClassWithDependencies>()).Return(the_sut);
            }

            protected override void because()
            {
                result = sut.create_sut();
            }


            [Observation]
            public void should_create_the_sut_by_using_the_sut_factory()
            {
                result.should_be_equal_to(the_sut);
            }
        }

        public class SampleTestWithAnSut : observations_for_an_instance_sut<AClassWithDependencies, AClassWithDependencies> {}
        public class AClassWithDependencies 
        {
        }
    }
}