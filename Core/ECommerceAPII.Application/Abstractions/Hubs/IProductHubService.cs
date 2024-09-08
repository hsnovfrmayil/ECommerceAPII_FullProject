using System;
namespace ECommerceAPII.Application.Abstractions.Hubs;

public interface IProductHubService
{
    Task ProductAddedMessageAsync(string message);
}

