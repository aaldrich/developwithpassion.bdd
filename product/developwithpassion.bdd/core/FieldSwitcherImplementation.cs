using System;
using System.Reflection;
using developwithpassion.bdd.mbunit.standard.observations;

namespace developwithpassion.bdd.core
{
    public class FieldSwitcherImplementation : FieldSwitcher
    {
        Type target;
        FieldInfo field;
        object original_value;

        public FieldSwitcherImplementation(Type target, FieldInfo field)
        {
            this.target = target;
            this.field = field;

            original_value = field.GetValue(target);
        }

        public PipelineBehaviour to(object new_value)
        {
            return new PipelineBehaviour(() => field.SetValue(target,new_value),
                () => field.SetValue(target,original_value));
        }
    }
}