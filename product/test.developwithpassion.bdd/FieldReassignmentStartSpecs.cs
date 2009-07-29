using System;
using System.Linq.Expressions;
using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;

namespace test.developwithpassion.bdd
{
    public class FieldReassignmentStartSpecs
    {
        public abstract class concern : observations_for_a_sut_without_a_contract<FieldReassignmentStart> {}

        [Concern(typeof (FieldReassignmentStart))]
        public class when_provided_the_target_which_it_is_going_to_be_changing : concern
        {
            context c = () =>
            {
                var target = typeof (TypeWithAStaticField);
                original_factory = FieldReassignmentStart.field_switcher_factory;
                switcher = an<FieldSwitcher>();
                FieldReassignmentStart.field_switcher_factory = (x, y) =>
                {
                    args = new Pair<Type, FieldInfo>(x, y);
                    return switcher;
                };
            };

            because b = () =>
            {
                result = sut.change(() => TypeWithAStaticField.some_value);
            };

            after_each_observation a = () =>
            {
                FieldReassignmentStart.field_switcher_factory = original_factory;
            };


            it should_return_a_field_changer_that_can_be_used_to_specify_the_value_for_during_testing = () =>
            {
                args.first.should_be_equal_to(typeof (TypeWithAStaticField));
                args.second.should_be_equal_to(typeof (TypeWithAStaticField).GetField("some_value",
                                                                                      BindingFlags.Public | BindingFlags.Static));
                result.should_be_equal_to(switcher);
            };

            static public Expression<Func<object>> item(Expression<Func<object>> target)
            {
                return target;
            }

            static string new_value;
            static FieldSwitcher result;
            static FieldSwitcher switcher;
            static FieldSwitcherFactory original_factory;
            static Pair<Type, FieldInfo> args;
        }
        [Concern(typeof (FieldReassignmentStart))]
        public class when_changing_the_target_that_requires_a_boxing_operation_to_be_performed : concern
        {
            context c = () =>
            {
                var target = typeof (TypeWithAStaticField);
                original_factory = FieldReassignmentStart.field_switcher_factory;
                switcher = an<FieldSwitcher>();
                FieldReassignmentStart.field_switcher_factory = (x, y) =>
                {
                    args = new Pair<Type, FieldInfo>(x, y);
                    return switcher;
                };
            };

            because b = () =>
            {
                result = sut.change(() => TypeWithAStaticField.some_value_that_will_be_boxed);
            };

            after_each_observation a = () =>
            {
                FieldReassignmentStart.field_switcher_factory = original_factory;
            };


            it should_return_a_field_changer_that_can_be_used_to_specify_the_value_for_during_testing = () =>
            {
                args.first.should_be_equal_to(typeof (TypeWithAStaticField));
                args.second.should_be_equal_to(typeof (TypeWithAStaticField).GetField("some_value_that_will_be_boxed",
                                                                                      BindingFlags.Public | BindingFlags.Static));
                result.should_be_equal_to(switcher);
            };

            static public Expression<Func<object>> item(Expression<Func<object>> target)
            {
                return target;
            }

            static string new_value;
            static FieldSwitcher result;
            static FieldSwitcher switcher;
            static FieldSwitcherFactory original_factory;
            static Pair<Type, FieldInfo> args;
        }
    }

    public class TypeWithAStaticField
    {
        static public string some_value = "blah";
        static public int some_value_that_will_be_boxed = 23;
    }
}