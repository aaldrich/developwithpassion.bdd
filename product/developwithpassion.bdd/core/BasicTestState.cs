using System;
using System.Collections.Generic;

namespace developwithpassion.bdd.core
{
    public interface BasicTestState : DependencyBag
    {
        IList<PipelineBehaviour> pipeline_behaviours { get; set; }
        object test { get; set; }
        Exception exception_thrown_while_the_sut_performed_its_work { get; set; }
        Action behaviour_performed_in_because { get; set; }
    }
}