using System;
using ECommerceAPII.Application.Abstractions.Hubs;
using ECommerceAPII.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceAPII.SignalR.HubServices;

public class ProductHubService : IProductHubService
{
    readonly IHubContext<ProductHub> _hubContext;

    public ProductHubService(IHubContext<ProductHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task ProductAddedMessageAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
    }
}

