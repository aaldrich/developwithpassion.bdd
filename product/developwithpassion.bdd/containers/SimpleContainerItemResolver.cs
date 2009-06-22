namespace developwithpassion.bdd.containers
{
    public class SimpleContainerItemResolver : ContainerItemResolver
    {
        readonly DependencyResolver resolver;

        public SimpleContainerItemResolver(DependencyResolver resolver)
        {
            this.resolver = resolver;
        }

        public object resolve()
        {
            return resolver();
        }
    }
}