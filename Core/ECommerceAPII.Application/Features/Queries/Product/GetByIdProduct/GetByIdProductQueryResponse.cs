using System;
using ECommerceAPII.Domain.Entities;

namespace ECommerceAPII.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryResponse
{
    public string? Name { get; set; }

    public int Stock { get; set; }

    public long Price { get; set; }
}

