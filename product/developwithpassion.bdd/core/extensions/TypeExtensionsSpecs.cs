using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;

namespace developwithpassion.bdd.core.extensions
{
    public class TypeExtensionSpecs
    {
        public abstract class concern : observations_for_a_static_sut {}

        [Concern(typeof (TypeExtensions))]
        public class when_a_type_is_told_to_find_its_greediest_constructor : concern
        {
            static ConstructorInfo result;

            because b = () =>
            {
                result = typeof (SomethingWithParameterfulConstructors).greediest_constructor();
            };

            it should_return_the_constructor_that_takes_the_most_arguments = () =>
            {
                result.GetParameters().Count().should_be_equal_to(2);
            };
        }

        [Concern(typeof (TypeExtensions))]
        public class when_a_generic_type_is_told_to_return_its_proper_name : concern
        {
            static string result;

            because b = () =>
            {
                result = typeof (List<int>).proper_name();
            };

            it should_return_a_name_that_has_its_generic_type_arguments_expanded = () =>
            {
                result.should_be_equal_to("List`1<System.Int32>");
            };
        }

        [Concern(typeof (TypeExtensions))]
        public class when_told_to_get_a_list_of_fields_of_certain_type : concern
        {
            static IEnumerable<FieldInfo> result;

            because b = () =>
            {
                result = TypeExtensions.all_fields_of<it>(typeof(SomethingWithParameterfulConstructors),BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            };

            it should_only_return_the_fields_that_match_the_expected_type = () =>
            {
                result.Count().should_be_equal_to(2);
            };
        }


        

        public class SomethingWithParameterfulConstructors
        {
            public IDbConnection connection { get; set; }

            public IDbCommand command { get; set; }

            public SomethingWithParameterfulConstructors(IDbConnection connection) {}

            public SomethingWithParameterfulConstructors(IDbConnection connection, IDbCommand command)
            {
                this.connection = connection;
                this.command = command;
            }

            it first_observation = () =>
            {
                


            };

            it second_observation = () =>
            {
                


            };
        }
    }
}