using System;
namespace ECommerceAPII.Application.Abstractions.Hubs;

public interface IOrderHubService
{
    Task OrderAddedMessageAsync(string message);
}

