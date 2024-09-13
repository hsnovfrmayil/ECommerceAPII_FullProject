using System;
using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.Abstractions.Storage;
using ECommerceAPII.Application.Abstractions.Token;
using ECommerceAPII.Infrastructure.Enums;
using ECommerceAPII.Infrastructure.Services;
using ECommerceAPII.Infrastructure.Services.Storage.Local;
using ECommerceAPII.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPII.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorageService,StorageService>();
        serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        serviceCollection.AddScoped<IMailService, MailService>();
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

