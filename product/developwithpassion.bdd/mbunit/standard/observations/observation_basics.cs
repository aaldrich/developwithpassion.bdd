using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.bdd.core;

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

        static public ChangeValueInPipeline change(Expression<Func<object>> static_expression)
        {
            return new ChangeValueInPipeline(add_pipeline_behaviour, static_expression);
        }

        public class ChangeValueInPipeline
        {
            Action<PipelineBehaviour> add_behaviour;
            Expression<Func<object>> static_expression;

            public ChangeValueInPipeline(Action<PipelineBehaviour> add_behaviour, Expression<Func<object>> static_expression)
            {
                this.add_behaviour = add_behaviour;
                this.static_expression = static_expression;
            }

            public void to(object new_value)
            {
                add_behaviour(new FieldReassignmentStart().change(static_expression).to(new_value));
            }
        }
    }
}