using System;

namespace developwithpassion.bdd.core
{
    public interface TestState<SUT> : BasicTestState
    {
        SUT sut { get; set; }
        Func<SUT> factory { get; set; }
        void run_teardown_pipeline();
    }
}