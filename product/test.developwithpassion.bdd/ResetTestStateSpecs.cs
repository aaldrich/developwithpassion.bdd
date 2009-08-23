using System;
using System.Collections.Generic;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd
{
    public class ResetTestStateSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<Command,
                                            ResetTestState<int>>
        {
            context c = () =>
            {
                state_implementation = an<TestState<int>>();
                state_implementation.dependencies = new Dictionary<Type, object>();
                controller = the_dependency<DelegateController>();
            };

            public override Command create_sut()
            {
                return new ResetTestState<int>(state_implementation);
            }

            static public DelegateController controller;
            static protected TestState<int> state_implementation;
        }

        [Concern(typeof (ResetTestState<int>))]
        public class when_test_test_state_is_reset : concern
        {
            it should_clear_the_dependencies_dictionary = () =>
            {
                state_implementation.dependencies.Count.should_be_equal_to(0);
            };
        }
    }
}