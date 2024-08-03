using Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, Func<string, object> getSectionFunc)
        {
            services.Configure<CacheOptions>(((IConfigurationSection)getSectionFunc("CacheOptions")).Bind);

            return services;
        }
    }
}
