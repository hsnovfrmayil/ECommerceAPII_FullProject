using System;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandRequest :IRequest<CreateProductCommandResponse>
{
    public string? Name { get; set; }

    public int Stock { get; set; }

    public long Price { get; set; }
}

