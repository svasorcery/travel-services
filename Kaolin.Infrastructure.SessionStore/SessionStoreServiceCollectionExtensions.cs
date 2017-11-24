using System;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    using Kaolin.Infrastructure.SessionStore;

    public static class SessionStoreServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemorySessionProvider(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAdd(ServiceDescriptor.Singleton<ISessionProvider, InMemorySessionProvider>());

            return services;
        }

        public static IServiceCollection AddMongoDbSessionProvider(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAdd(ServiceDescriptor.Singleton<ISessionProvider, MongoDbSessionProvider>());

            return services;
        }
    }
}
