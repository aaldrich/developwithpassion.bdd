namespace developwithpassion.bdd.mbunit.standard.observations
{
    public abstract class observations_for_an_instance_sut<ContractUnderTest, ClassUnderTest> : an_observations_set_of_basic_behaviours<ContractUnderTest> where ClassUnderTest : ContractUnderTest
    {
        public override ContractUnderTest create_sut()
        {
            return observation_context.build_sut<ContractUnderTest, ClassUnderTest>();
        }

        static protected Dependency the_dependency<Dependency>() where Dependency : class
        {
            return observation_context.system_under_test_dependency_builder.the_dependency<Dependency>();
        }

        static protected void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            observation_context.system_under_test_dependency_builder.provide_a_basic_sut_constructor_argument(value);
        }
    }
}