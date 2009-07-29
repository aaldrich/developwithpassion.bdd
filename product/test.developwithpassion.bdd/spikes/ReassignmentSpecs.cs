 
using System;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdd.mbunit;

namespace test.developwithpassion.bdd.spikes
{
    public class ReassignmentSpecs
    {
        public abstract class concern : observations_for_a_static_sut {}

        [Concern(typeof (Change))]
        public class when_reassigning_a_field : concern
        {
            context c = () =>
            {
                change(() => TypeWithStatic.some_value).to(CreateDelegate.of<Action>(() => ran = true));
            };

            it should_change_the_value_of_the_static_field = () =>
            {
                TypeWithStatic.some_value();
                ran.should_be_true();
            };

            static bool ran;
        }

        public class TypeWithStatic
        {
            static public Action some_value = () =>
            {
                throw new Exception();
            };
        }
    }
}
