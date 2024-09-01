using System;
using ECommerceAPII.Application.RequestParameters;
using MediatR;

namespace ECommerceAPII.Application.Features.Queries.Product.GetAllProducts;

public class GetAllProductQueryRequest :IRequest<GetAllProductQueryResponse>
{
    public int Page { get; set; } = 1;

    public int Size { get; set; } = 5;
}

