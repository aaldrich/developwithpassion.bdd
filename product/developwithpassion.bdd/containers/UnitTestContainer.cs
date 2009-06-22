using System;
using System.Collections.Generic;
using developwithpassion.commons.core.infrastructure.containers;

namespace developwithpassion.bdd.containers
{
    public class UnitTestContainer
    {
        static Container container;
        static IDictionary<Type, ContainerItemResolver> items;
        static object mutex = new object();


        static public void add_implementation_of<Interface>(Interface implementation)
        {
            do_in_initialized_container(
                () => items.Add(typeof (Interface), new SimpleContainerItemResolver(() => implementation)));
        }

        static void do_in_initialized_container(Action action)
        {
            perform_blocking_action(() =>
            {
                if (container == null)
                {
                    items = new Dictionary<Type, ContainerItemResolver>();
                    container = new SimpleContainer(items);
                    IOC.initialize_with(container);
                }
                action();
            });
        }

        static void perform_blocking_action(Action action)
        {
            lock (mutex) action();
        }


        static public void tear_down()
        {
            perform_blocking_action(() =>
            {
                items = null;
                container = null;
                IOC.initialize_with(null);
            });
        }
    }
}