using System;
using ECommerceAPI.Persistence;
using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.Abstractions.Services.Authentications;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Domain.Entities.Identity;
using ECommerceAPII.Persistence.Contexts;
using ECommerceAPII.Persistence.Repositories;
using ECommerceAPII.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPII.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ECommerceAPIIDbContext>(options=>
                    options.UseNpgsql(Configuration.ConnectionString()),ServiceLifetime.Singleton);
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<ECommerceAPIIDbContext>();

        services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();
        services.AddSingleton<IProductReadRepository, ProductReadRepository>();
        services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();
        services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
        services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
        services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddSingleton<IBasketReadRepository, BasketReadRepository>();
        services.AddSingleton<IBasketWriteRepository, BasketWriteRepository>();
        services.AddSingleton<IBasketItemReadRepository, BasketItemReadRepository>();
        services.AddSingleton<IBasketItemWriteRepository, BasketItemWriteRepository>();

        services.AddSingleton<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddSingleton<IProductImageFileWriteRepository,ProductImageFileWriteRepository>();
        services.AddSingleton<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddSingleton<IInvoiceFileWriteRepository,InvoiceFileWriteRepository>();
        services.AddSingleton<IFileReadRepository,FileReadRepository>();
        services.AddSingleton<IFileWriteRepository,FileWriteRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IInternalAuthentication, AuthService>();
        services.AddScoped < IExternalAuthentication, AuthService>();

    }
}

