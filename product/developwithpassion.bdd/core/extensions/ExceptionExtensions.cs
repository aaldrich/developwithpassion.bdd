using System;

namespace developwithpassion.bdd.core.extensions
{
    static public class ExceptionExtensions
    {
        static public Exception get_exception(this Action work)
        {
            try
            {
                work();
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}