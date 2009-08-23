using System.Collections.Generic;
using System.Linq;

namespace test.developwithpassion.bdd.utiliity
{
    public class ObjectMother
    {
        public static IEnumerable<T> enumerable_from<T>(params T[] items) {
            return items.Select(item => item);
        }
    }
}