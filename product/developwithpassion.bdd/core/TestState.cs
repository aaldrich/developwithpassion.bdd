using System;
using System.Collections.Generic;
using developwithpassion.bdd.mbunit.standard.observations;

namespace developwithpassion.bdd.core
{
    public interface TestState<SUT> {
        IList<PipelineBehaviour> pipeline_behaviours { get; set; }
        object test { get; set; }
        IDictionary<Type, object> dependencies { get; set; }
        Exception exception_thrown_while_the_sut_performed_its_work { get; set; }
        Action behaviour_performed_in_because { get; set; }
        SUT sut { get; set; }
        Func<SUT> factory { get; set; }
        void run_teardown_pipeline();
    }
}