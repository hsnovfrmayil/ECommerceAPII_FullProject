using System;
using ECommerceAPII.Application.DTOs.Order;

namespace ECommerceAPII.Application.Abstractions.Services;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrder createOrder);

    Task<ListOrder> GetAllOrdersAsync(int page,int size);

    Task<SingleOrder> GetOrderByIdAsync(string id );

    Task CompletedOrderAsync(string id);
}

