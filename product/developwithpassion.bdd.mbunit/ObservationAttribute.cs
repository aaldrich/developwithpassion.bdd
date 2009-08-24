using System;
using MbUnit.Core.Framework;

namespace developwithpassion.bdd.mbunit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ObservationAttribute : TestPatternAttribute {}
}