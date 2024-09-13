using System;
using MediatR;

namespace ECommerceAPII.Application.Features.Queries.Basket.GetBasketItems;

public class GetBasketItemsQueryRequest :IRequest<List<GetBasketItemsQueryResponse>>
{

}

