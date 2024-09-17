using System;
using ECommerceAPII.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.Order.CompletedOrder;

public class CompletedOrderCommandHandler : IRequestHandler<CompletedOrderCommandRequest, CompletedOrderCommandResponse>
{
    readonly IOrderService _orderService;

    public CompletedOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
    {
        await  _orderService.CompletedOrderAsync(request.Id);
        return new();
    }
}

