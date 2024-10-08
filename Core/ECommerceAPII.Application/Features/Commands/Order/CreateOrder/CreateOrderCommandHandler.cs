﻿using System;
using ECommerceAPII.Application.Abstractions.Hubs;
using ECommerceAPII.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.Order.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
{
    readonly IOrderService _orderService;
    readonly IBasketService _basketService;
    readonly IOrderHubService _orderHubService;

    public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
    {
        _orderService = orderService;
        _basketService = basketService;
        _orderHubService = orderHubService;
    }

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        await _orderService.CreateOrderAsync(new()
        {
            Description=request.Description,
            Address=request.Address,
            BasketId=_basketService.GetUserActiveBasket?.Id.ToString()
        });

        await _orderHubService.OrderAddedMessageAsync("yaxsi oglan, yeni sifaris elave edildi, agilli ol yeni!");

        return new();
    }
}

