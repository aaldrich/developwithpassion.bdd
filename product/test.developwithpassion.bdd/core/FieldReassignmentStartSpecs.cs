using System;
using System.Linq.Expressions;
using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.harnesses.mbunit;
using developwithpassion.bdddoc.core;
using Rhino.Mocks;

namespace test.developwithpassion.bdd.core
{
    public class FieldReassignmentStartSpecs
    {
        public abstract class concern : observations_for_a_sut_without_a_contract<FieldReassignmentStart> {


            context c = () =>
            {
                member_info = typeof (TypeWithAStaticField).GetField("some_value");
                switcher_factory = the_dependency<FieldSwitcherFactory>();
                switcher = an<FieldSwitcher>();
                switcher_factory.Stub(x => x.create_to_target(Arg<MemberInfo>.Is.NotNull)).Return(switcher);
            };

            static protected MemberInfo member_info;
            static protected FieldSwitcherFactory switcher_factory;
            static protected FieldSwitcher switcher;
        }

        [Concern(typeof (FieldReassignmentStart))]
        public class when_provided_the_target_which_it_is_going_to_be_changing : concern
        {
            context c = () =>
            {
                var target = typeof (TypeWithAStaticField);

            };

            because b = () =>
            {
                result = sut.change(() => TypeWithAStaticField.some_value);
            };

            it should_return_a_field_changer_that_can_be_used_to_specify_the_value_for_during_testing = () =>
            {
                result.should_be_equal_to(switcher);
            };

            static public Expression<Func<object>> item(Expression<Func<object>> target)
            {
                return target;
            }

            static string new_value;
            static FieldSwitcher result;
        }
        [Concern(typeof (FieldReassignmentStart))]
        public class when_changing_the_target_that_requires_a_boxing_operation_to_be_performed : concern
        {
            context c = () =>
            {
                var target = typeof (TypeWithAStaticField);
                boxed_member_info = typeof (TypeWithAStaticField).GetField("some_value_that_will_be_boxed");
            };

            because b = () =>
            {
                result = sut.change(() => TypeWithAStaticField.some_value_that_will_be_boxed);
            };

            it should_return_a_field_changer_that_can_be_used_to_specify_the_value_for_during_testing = () =>
            {
                result.should_be_equal_to(switcher);
            };

            static public Expression<Func<object>> item(Expression<Func<object>> target)
            {
                return target;
            }

            static string new_value;
            static FieldSwitcher result;
            static FieldSwitcherFactory original_factory;
            static FieldInfo boxed_member_info;
        }
    }

    public class TypeWithAStaticField
    {
        static public string some_value = "blah";
        static public int some_value_that_will_be_boxed = 23;
    }
}