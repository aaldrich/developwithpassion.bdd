using System;
using System.Collections.Generic;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core
{
    public class TestStateImplementation<SUT> : TestState<SUT>
    {
        public IList<PipelineBehaviour> pipeline_behaviours { get; set; }
        public object test { get; set; }
        public IDictionary<Type, object> dependencies { get; set; }
        public Exception exception_thrown_while_the_sut_performed_its_work { get; set; }
        public Action behaviour_performed_in_because { get; set; }
        public SUT sut { get; set; }
        public Func<SUT> factory { get; set; }

        public TestStateImplementation(object test, Func<SUT> factory)
        {
            this.test = test;
            this.factory = factory;

            pipeline_behaviours = new List<PipelineBehaviour>();
            dependencies = new Dictionary<Type, object>();
        }

        public void run_teardown_pipeline()
        {
            pipeline_behaviours.each(item => item.finish());
        }
    }
}