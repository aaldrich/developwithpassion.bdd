using System;

namespace developwithpassion.bdd.core
{
    public interface TestState<SUT> : BasicTestState
    {
        SUT sut { get; set; }
        Func<SUT> factory { get; set; }
        void run_teardown_pipeline();
        void clear_test_pipeline();
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void reset();
    }
}