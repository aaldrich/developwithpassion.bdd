using System;
using System.Reflection;

namespace developwithpassion.bdd.core
{
    public delegate FieldSwitcher FieldSwitcherFactory(Type target,FieldInfo field);
}