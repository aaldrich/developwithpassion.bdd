using System;

namespace developwithpassion.bdd.mbunit.standard.observations
{
    public class PipelineBehaviour
    {
        Action tear_down;
        Action context;

        public PipelineBehaviour(Action context, Action tear_down)
        {
            this.context = context;
            this.tear_down = tear_down;
        }

        public void start() {
            context();
        }

        public void finish() {
            tear_down();
        }
    }
}