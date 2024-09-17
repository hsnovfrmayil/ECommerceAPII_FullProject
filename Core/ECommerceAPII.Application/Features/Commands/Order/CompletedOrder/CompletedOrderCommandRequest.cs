using System;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.Order.CompletedOrder;

public class CompletedOrderCommandRequest :IRequest<CompletedOrderCommandResponse>
{
    public string Id { get; set; }
}

