using System;
using System.Collections.Generic;
using developwithpassion.bdd.containers;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.mbunit.standard.observations;

namespace developwithpassion.bdd.core
{
    public class ResetTestState<SUT> : Command
    {
        TestState<SUT> test_state;

        public ResetTestState(TestState<SUT> test_state)
        {
            this.test_state = test_state;
        }

        public void run()
        {
            test_state.pipeline_behaviours.Clear();
            test_state.pipeline_behaviours.Add(new PipelineBehaviour(() => {}, UnitTestContainer.tear_down));
            test_state.behaviour_performed_in_because = null;
            test_state.exception_thrown_while_the_sut_performed_its_work = null;
            test_state.dependencies = new Dictionary<Type, object>();
        }
    }
}