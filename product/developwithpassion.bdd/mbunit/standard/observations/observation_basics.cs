using System;
using System.Collections.Generic;
using developwithpassion.bdd.concerns;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    public abstract class observation_basics
    {
        static protected IList<Action> context_pipeline = new List<Action>();
        static protected IList<Action> teardown_pipeline = new List<Action>();

        static public void add_pipeline_behaviour(PipelinePair pipeline_pair)
        {
            context_pipeline.Add(pipeline_pair.context);
            teardown_pipeline.Add(pipeline_pair.tear_down);
        }

        static  public void add_pipeline_behaviour(Action context,Action teardown) {
            add_pipeline_behaviour(new PipelinePair(context, teardown));
        } 
    }
}