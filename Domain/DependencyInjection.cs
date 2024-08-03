using Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheOptions>(configuration.GetSection("CacheOptions").Bind);
            services.Configure<HangfireOptions>(configuration.GetSection("HangfireOptions").Bind);

            return services;
        }
    }
}
