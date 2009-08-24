using developwithpassion.bdd.mocking.rhino;

namespace developwithpassion.bdd.mbunit
{
    [Observations]
    public abstract class an_observations_set_of_basic_behaviours<SUT> : sut_observation_context<SUT, SUT, RhinoMocksMockFactory> {}
}