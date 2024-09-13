using System;
namespace ECommerceAPII.Application.Features.Commands.Order.CreateOrder;

public class CreateOrderCommandResponse
{
    public string OrderCode { get; set; }

    public string UserName { get; set; }

    public float TotalPrice { get; set; }

    public DateTime CreatedDate { get; set; }
}

