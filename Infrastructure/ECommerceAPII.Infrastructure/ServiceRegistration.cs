using System;
using ECommerceAPII.Application.Abstractions.Storage;
using ECommerceAPII.Infrastructure.Enums;
using ECommerceAPII.Infrastructure.Services;
using ECommerceAPII.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPII.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorageService,StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T:class, IStorage
    {
        serviceCollection.AddScoped<IStorage, T >();
    }

    public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            case StorageType.Azure:
                break;
            case StorageType.AWS:
                break;
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}

