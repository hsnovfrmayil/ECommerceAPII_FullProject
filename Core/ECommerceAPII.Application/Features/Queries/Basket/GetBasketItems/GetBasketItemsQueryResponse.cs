using System;
namespace ECommerceAPII.Application.Features.Queries.Basket.GetBasketItems;

public class GetBasketItemsQueryResponse
{
    public string BasketItemId { get; set; }

    public string Name { get; set; }

    public long Price { get; set; }

    public float Quantity { get; set; }
}

