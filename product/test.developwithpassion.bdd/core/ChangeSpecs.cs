using System.Security.Principal;
using System.Threading;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd.core
{
    public class ChangeSpecs
    {
        public abstract class concern : observations_for_a_static_sut {}

        [Concern(typeof (Change))]
        public class when_a_change_is_made_within_a_context_block : concern
        {
            context c = () =>
            {
                new_value = "other_value";
                change(() => SomeStatic.some_value).to(new_value);
            };


            it should_have_caused_the_change_to_the_field = () =>
            {
                SomeStatic.some_value.should_be_equal_to(new_value);
            };

            static string new_value;
        }

        public class integration
        {
            [Concern(typeof (Change))]
            public class when_swapping_the_thread_current_principal : concern
            {
                context c = () =>
                {
                    principal = an<IPrincipal>();
                    change(() => Thread.CurrentPrincipal).to(principal);
                };


                it should_have_the_fake_principal_being_used_as_the_principal = () =>
                {
                    Thread.CurrentPrincipal.should_be_equal_to(principal);
                };

                static string new_value;
                static IPrincipal principal;
                static object result;
            }
        }

        public class SomeStatic
        {
            static public string some_value = "lah";
        }
    }
}