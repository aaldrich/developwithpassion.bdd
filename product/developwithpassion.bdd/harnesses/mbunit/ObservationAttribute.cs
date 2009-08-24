using System;
using MbUnit.Core.Framework;

namespace developwithpassion.bdd.harnesses.mbunit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ObservationAttribute : TestPatternAttribute {}
}