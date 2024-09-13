using System;
using MediatR;

namespace ECommerceAPII.Application.Features.Queries.Order.GetAllOrders;

public class GetAllOrdersQueryRequest :IRequest<GetAllOrdersQueryResponse>
{
    public int Page { get; set; } = 1;

    public int Size { get; set; } = 5;
}

