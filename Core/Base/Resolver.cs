using Autofac;

namespace Core.Base
{
    public static class Resolver
    {
        private static IContainer _container;

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}