using System;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.Basket.RemoveBasketItem;

public class RemoveBasketItemCommandRequest :IRequest<RemoveBasketItemCommandResponse>
{
    public string BasketItemId { get; set; } 
}

