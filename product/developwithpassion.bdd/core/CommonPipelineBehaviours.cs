using developwithpassion.bdd.containers;

namespace developwithpassion.bdd.core
{
    public class CommonPipelineBehaviours
    {
        public static readonly PipelineBehaviour tear_down_the_unit_test_container = new PipelineBehaviour(() => {},
            UnitTestContainer.tear_down);
    }
}