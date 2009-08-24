using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.harnesses.mbunit
{
    public class AppendTestsToTestTree : ParameterizedCommand<TestTreeArgs<it>>
    {
        const BindingFlags binding_flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance;

        public void run_against(TestTreeArgs<it> item)
        {
            item.type_that_contains_tests
                .all_fields_of<it>(binding_flags)
                .each(field => item.tree.AddChild(item.parent, new DelegateRunInvoker(item.run, field)));
        }
    }
}