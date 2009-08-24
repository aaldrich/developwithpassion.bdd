using System;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.harnesses.mbunit;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd.core
{
    public class DeferredValueSpecs
    {
        public abstract class concern : observations_for_a_sut_without_a_contract<DeferredValue<int>> {}

        [Concern(typeof (DeferredValue<int>))]
        public class when_implicitly_converted_to_its_result : concern
        {
            context c = () =>
            {
                number_to_change = 23;
                provide_a_basic_sut_constructor_argument<Func<int>>(() => number_to_change);
                number_to_change = 44;
            };

            because b = () =>
            {
                result = sut;
            };


            it should_return_the_most_current_value_of_that_value = () =>
            {
                result.should_be_equal_to(44);
            };

            static int result;
            static int number_to_change;
        }
    }
}