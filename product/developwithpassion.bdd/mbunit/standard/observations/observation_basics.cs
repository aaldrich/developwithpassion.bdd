using System;
using System.Collections.Generic;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    public abstract class observation_basics
    {
        static protected IList<PipelineBehaviour> context_pipeline = new List<PipelineBehaviour>();

        static public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            context_pipeline.Add(pipeline_behaviour);
        }

        static public void add_pipeline_behaviour(Action context, Action teardown)
        {
            add_pipeline_behaviour(new PipelineBehaviour(context, teardown));
        }
    }
}