using developwithpassion.bdd.core.observations;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mocking.rhino;
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
            protected ObservationController<AClassWithDependencies, AClassWithDependencies, RhinoMocksMockFactory> controller;

            [SetUp]
            public void setup()
            {
                sut = new SampleTestWithAnSut();

                controller = MockRepository.GenerateStub<ObservationController<AClassWithDependencies, AClassWithDependencies, RhinoMocksMockFactory>>();
                sut_observation_context<AClassWithDependencies, AClassWithDependencies, RhinoMocksMockFactory>.observation_controller = controller;
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
                controller.Stub(x => x.build_sut<AClassWithDependencies, AClassWithDependencies>()).Return(the_sut);
            }

            protected override void because()
            {
                result = sut.create_sut();
            }


            [Observation]
            public void should_tell_the_controller_to_create_the_sut()
            {
                result.should_be_equal_to(the_sut);
            }
        }

        public class SampleTestWithAnSut : observations_for_an_instance_sut<AClassWithDependencies, AClassWithDependencies> {}

        public class AClassWithDependencies {}
    }
}