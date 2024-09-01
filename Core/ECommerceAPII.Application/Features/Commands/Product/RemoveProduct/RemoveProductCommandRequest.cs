using System;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.Product.RemoveProduct;

public class RemoveProductCommandRequest :IRequest<RemoveProductCommandResponse>
{
    public string Id { get; set; }
}

