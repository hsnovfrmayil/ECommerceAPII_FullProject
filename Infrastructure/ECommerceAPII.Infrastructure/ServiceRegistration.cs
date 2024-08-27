using System;
using ECommerceAPII.Application.Services;
using ECommerceAPII.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPII.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFileService, FileService>();
    }
}

