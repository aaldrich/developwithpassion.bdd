using System;

namespace developwithpassion.bdd.core
{
    public class DeferredValue<T>
    {
        Func<T> value_resolver;

        public DeferredValue(Func<T> value)
        {
            this.value_resolver = value;
        }

        static public implicit operator T(DeferredValue<T> value) {

            return value.value_resolver();
        }

    }
}