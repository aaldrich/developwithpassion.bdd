using System;
using developwithpassion.bdd.mocking.rhino;

namespace developwithpassion.bdd.harnesses.mbunit
{
    public abstract class observations_for_an_instance_sut<ContractUnderTest, ClassUnderTest> : sut_observation_context<ContractUnderTest,ClassUnderTest,RhinoMocksMockFactory> where ClassUnderTest : ContractUnderTest
    {
        [Obsolete("use context property to access testing controller")]
        public override ContractUnderTest create_sut()
        {
            return observation_controller.build_sut<ContractUnderTest,ClassUnderTest>();
        }

        [Obsolete("use context property to access testing controller")]
        static protected Dependency the_dependency<Dependency>() where Dependency : class
        {
            return observation_controller.the_dependency<Dependency>();
        }

        [Obsolete("use context property to access testing controller")]
        static protected void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            observation_controller.provide_a_basic_sut_constructor_argument(value);
        }
    }
}