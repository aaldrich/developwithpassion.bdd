using System;
using developwithpassion.bdd.core.extensions;
using MbUnit.Framework;

namespace developwithpassion.bdd.harnesses.mbunit
{
    static public class AssertionExtensions
    {
        static public void should_be_null(this object item)
        {
            Assert.IsNull(item);
        }

        static public void should_not_be_null(this object item)
        {
            Assert.IsNotNull(item);
        }

        static public void should_be_true(this bool item)
        {
            item.should_be_equal_to(true);
        }

        static public void should_be_false(this bool item)
        {
            item.should_be_equal_to(false);
        }
        
        static public void should_not_throw_any_exceptions(this Action work_to_perform)
        {
            work_to_perform();
        }

        public static ExceptionType should_throw_an<ExceptionType>(this Action work_to_perform) where ExceptionType : Exception
        {
            var resultingException = work_to_perform.get_exception();
            resultingException.should_not_be_null();
            resultingException.should_be_an_instance_of<ExceptionType>();
            return (ExceptionType)resultingException;
        }

        static public Type should_be_an_instance_of<Type>(this object item)
        {
            return item.should_be_an<Type>();
        }

        static public Type should_be_an<Type>(this object item)
        {
            Assert.IsInstanceOfType(typeof (Type), item);
            return (Type) item;
        }

        public static void should_not_be_an_instance_of<Type>(this object item)
        {
            Assert.IsNotInstanceOfType(typeof(Type), item);
            return;
        }

    }
}