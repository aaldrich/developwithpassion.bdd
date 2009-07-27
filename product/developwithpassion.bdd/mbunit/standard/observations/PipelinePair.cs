using System;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    public class PipelinePair
    {
        public PipelinePair(Action context, Action tear_down)
        {
            this.context = context;
            this.tear_down = tear_down;
        }

        public Action context { get; private set; }
        public Action tear_down { get; private set; }
    }
}