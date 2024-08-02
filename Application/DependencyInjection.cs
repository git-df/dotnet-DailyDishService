﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Application.Providers;
using Domain.Interfaces;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IRestourantProvider, GoodFoodRestourantProvider>();
            services.AddScoped<IRestourantProvider, CafeAndRockRestourantProvider>();

            return services;
        }
    }
}
