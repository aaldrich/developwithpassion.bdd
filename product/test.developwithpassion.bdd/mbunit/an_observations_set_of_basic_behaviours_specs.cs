using System;
using System.Data;
using System.Data.SqlClient;
using developwithpassion.bdd.core.observations;
using developwithpassion.bdd.harnesses.mbunit;
using developwithpassion.bdd.mocking.rhino;
using developwithpassion.bdddoc.core;
using MbUnit.Framework;
using Rhino.Mocks;

namespace test.developwithpassion.bdd.mbunit
{
    public class an_observations_set_of_basic_behaviours_specs
    {
        [Observations]
        public abstract class concern
        {
            protected SampleSetOfObservations sut;
            protected ObservationController<IDbConnection, IDbConnection, RhinoMocksMockFactory> controller;

            [SetUp]
            public void setup()
            {
                controller = MockRepository.GenerateStub<ObservationController<IDbConnection, IDbConnection, RhinoMocksMockFactory>>();
                sut = new SampleSetOfObservations();

                sut_observation_context<IDbConnection, IDbConnection, RhinoMocksMockFactory>.observation_controller = controller;

                establish_context();
                because();
            }

            protected virtual void establish_context() {}
            protected abstract void because();
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_told_to_setup : concern
        {
            protected override void because()
            {
                sut.setup();
            }


            [Observation]
            public void should_tell_the_controller_to_reset()
            {
                controller.received(x => x.reset());
            }
        }


        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_told_to_teardown : concern
        {
            protected override void because()
            {
                sut.tear_down();
            }

            [Observation]
            public void should_tell_the_controller_to_tear_down()
            {
                controller.received(x => x.tear_down());
            }
        }

        public abstract class concern_for_an_observations_set_of_basic_behaviours_that_has_run_its_setup : concern {}

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_it_is_asked_for_the_exception_that_was_thrown :
            concern_for_an_observations_set_of_basic_behaviours_that_has_run_its_setup
        {
            Exception result;
            Exception exception;

            protected override void establish_context()
            {
                exception = new Exception();
                controller.Stub(x => x.exception_thrown_by_the_sut).Return(exception);
            }

            protected override void because()
            {
                result = sut_observation_context<IDbConnection, IDbConnection, RhinoMocksMockFactory>.exception_thrown_by_the_sut;
            }

            [Observation]
            public void should_return_the_exception_from_the_observation()
            {
                result.should_be_equal_to(exception);
            }
        }


        public class SampleSetOfObservations : an_observations_set_of_basic_behaviours<IDbConnection>
        {
            public override IDbConnection create_sut()
            {
                return new SqlConnection();
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_its_doing_method_is_leveraged : concern
        {
            static Action action = () => {};

            protected override void because()
            {
                sut_observation_context<IDbConnection, IDbConnection, RhinoMocksMockFactory>.doing(action);
            }

            [Observation]
            public void should_tell_the_observation_to_wrap_the_doing()
            {
                controller.received(x => x.doing(action));
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_its_an_method_is_used_to_create_a_mock : concern
        {
            IDbConnection result;
            IDbConnection connection;

            protected override void establish_context()
            {
                connection = MockRepository.GenerateStub<IDbConnection>();
                controller.Stub(x => x.an<IDbConnection>()).Return(connection);
            }

            protected override void because()
            {
                result = sut_observation_context<IDbConnection, IDbConnection, RhinoMocksMockFactory>.an<IDbConnection>();
            }

            [Observation]
            public void should_return_the_mock_created_by_the_observation()
            {
                result.should_be_equal_to(connection);
            }
        }
    }
}