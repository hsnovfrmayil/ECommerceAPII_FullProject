using System;
using ECommerceAPII.Application.Abstractions.Hubs;
using ECommerceAPII.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPII.SignalR;

public static class ServiceRegistration
{
     public static void AddSignalRServices(this IServiceCollection collection )
    {
        collection.AddTransient<IProductHubService, ProductHubService>();
        collection.AddTransient<IOrderHubService, OrderHubService>();
        collection.AddSignalR();
    }
}

