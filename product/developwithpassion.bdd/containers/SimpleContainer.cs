using System;
using System.Collections.Generic;
using developwithpassion.commons.core.infrastructure.containers;

namespace developwithpassion.bdd.containers
{
    public class SimpleContainer : Container
    {
        readonly IDictionary<Type, ContainerItemResolver> resolvers;

        public SimpleContainer(IDictionary<Type, ContainerItemResolver> resolvers)
        {
            this.resolvers = resolvers;
        }

        public Interface instance_of<Interface>()
        {
            return (Interface) instance_of(typeof (Interface));
        }

        public object instance_of(Type dependency_type)
        {
            return get_resolver_for(dependency_type).resolve();
        }

        public IEnumerable<Contract> all_instances_of<Contract>()
        {
            throw new NotImplementedException();
        }

        public void add_resolver_for<Interface>(ContainerItemResolver resolver)
        {
            resolvers.Add(typeof (Interface), resolver);
        }

        ContainerItemResolver get_resolver_for(Type type)
        {
            return resolvers[type];
        }

        public void add_resolver<T>(DependencyResolver resolver)
        {
            add_resolver_for<T>(new SimpleContainerItemResolver(resolver));
        }
    }
}