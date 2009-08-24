using MbUnit.Framework;

namespace developwithpassion.bdd.mbunit
{
    public class SlowAttribute : FixtureCategoryAttribute
    {
        public SlowAttribute() : base("SLOW") {}
    }
}