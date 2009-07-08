using MbUnit.Framework;

namespace developwithpassion.bdd.mbunit.standard
{
    public class SlowAttribute : FixtureCategoryAttribute
    {
        public SlowAttribute() : base("SLOW") {}
    }
}