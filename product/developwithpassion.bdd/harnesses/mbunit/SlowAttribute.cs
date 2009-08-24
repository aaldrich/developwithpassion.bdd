using MbUnit.Framework;

namespace developwithpassion.bdd.harnesses.mbunit
{
    public class SlowAttribute : FixtureCategoryAttribute
    {
        public SlowAttribute() : base("SLOW") {}
    }
}