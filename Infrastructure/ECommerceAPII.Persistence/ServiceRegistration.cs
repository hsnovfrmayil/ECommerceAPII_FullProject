using System;
using ECommerceAPI.Persistence;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Persistence.Contexts;
using ECommerceAPII.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPII.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ECommerceAPIIDbContext>(options=>
                    options.UseNpgsql(Configuration.ConnectionString()),ServiceLifetime.Singleton);


        services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();
        services.AddSingleton<IProductReadRepository, ProductReadRepository>();
        services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();
        services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
        services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
        services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();

        services.AddSingleton<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddSingleton<IProductImageFileWriteRepository,ProductImageFileWriteRepository>();
        services.AddSingleton<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddSingleton<IInvoiceFileWriteRepository,InvoiceFileWriteRepository>();
        services.AddSingleton<IFileReadRepository,FileReadRepository>();
        services.AddSingleton<IFileWriteRepository,FileWriteRepository>();

    }
}

