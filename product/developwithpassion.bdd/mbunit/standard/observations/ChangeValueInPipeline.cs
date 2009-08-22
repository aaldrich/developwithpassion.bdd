using System;
using System.Linq.Expressions;
using developwithpassion.bdd.core;

namespace developwithpassion.bdd.mbunit.standard.observations
{
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