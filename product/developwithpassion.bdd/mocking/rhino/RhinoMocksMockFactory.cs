using System;
using developwithpassion.bdd.core;
using Rhino.Mocks;

namespace developwithpassion.bdd.mocking.rhino
{
    public class RhinoMocksMockFactory : MockFactory
    {
        public Dependency create_stub<Dependency>() where Dependency : class
        {
            return MockRepository.GenerateStub<Dependency>();
        }

        public object create_stub(Type type)
        {
            return MockRepository.GenerateStub(type);
        }
    }
}