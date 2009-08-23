using developwithpassion.bdd.core;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    public class ObservationContextArgs<Contract>
    {
        public TestState<Contract> state { get; set; }
        public MockFactory mock_factory { get; set; }
        public object test { get; set; }
    }
}