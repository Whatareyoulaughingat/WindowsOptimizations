using Splat;

namespace WindowsOptimizations.Core.Extensions
{
    public static class LocatorExtensions
    {
        public static TService GetAnyService<TService>(this IDependencyResolver resolver, string? contract = null) where TService : class, new()
        {
            if (resolver.GetService<TService>() == null)
            {
                resolver.RegisterConstant(new TService());
            }

            return resolver.GetService<TService>();
        }
    }
}
