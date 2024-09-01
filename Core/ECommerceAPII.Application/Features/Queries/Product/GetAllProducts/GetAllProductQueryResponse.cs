using System;
namespace ECommerceAPII.Application.Features.Queries.Product.GetAllProducts;

public class GetAllProductQueryResponse
{
    public int TotalCount { get; set; }

    public object Products { get; set; }
}

