using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdddoc.core;
 using developwithpassion.bdd.mbunit;

namespace test.developwithpassion.bdd.core
{
    public class PipelineBehaviourSpecs
    {
        public abstract class concern : observations_for_a_sut_without_a_contract<PipelineBehaviour>
        {
        
        }

        [Concern(typeof(PipelineBehaviour))]
        public class when_told_to_start : concern
        {
            because b = () =>
            {
                sut.start(); 
            };

            public override PipelineBehaviour create_sut()
            {
                return new PipelineBehaviour(() => context_ran =true,() => teardown_ran = true);
            }

            it should_only_run_its_context_block = () =>
            {
                context_ran.should_be_true(); 
                teardown_ran.should_be_false(); 
            
            };

            static bool context_ran;
            static bool teardown_ran;
        }
        [Concern(typeof(PipelineBehaviour))]
        public class when_told_to_finish : concern
        {
            because b = () =>
            {
                sut.finish(); 
            };

            public override PipelineBehaviour create_sut()
            {
                return new PipelineBehaviour(() => context_ran =true,() => teardown_ran = true);
            }

            it should_only_run_its_teardown_block = () =>
            {
                context_ran.should_be_false(); 
                teardown_ran.should_be_true(); 
            };

            static bool context_ran;
            static bool teardown_ran;
        }
    }
}