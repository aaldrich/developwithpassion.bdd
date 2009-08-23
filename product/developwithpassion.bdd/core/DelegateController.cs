using System;
using System.Collections.Generic;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.mbunit.standard.observations;

namespace developwithpassion.bdd.core
{
    public interface DelegateController
    {
        void run_block<T>();
    }

    public class DelegateControllerImplementation : DelegateController
    {
        object test;

        public DelegateControllerImplementation(object test)
        {
            this.test = test;
        }

        public void run_block<T>()
        {
            build_command_chain<T>(test).run();
        }

        Command build_command_chain<DelegateType>(object raw_test)
        {
            var actions = new Stack<Command>();
            var current_class = raw_test.GetType();

            while (current_class.is_a_concern_type())
            {
                actions.Push(new DelegateFieldInvocation(typeof (DelegateType), this, current_class));
                current_class = current_class.BaseType;
            }

            return actions.as_command_chain();
        }
    }

    static public class ConcernExtensions
    {
        static public bool is_a_concern_type(this Type type)
        {
            return typeof (Observations)
                .IsAssignableFrom(type);
        }
    }
}