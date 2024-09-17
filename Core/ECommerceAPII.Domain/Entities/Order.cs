using System;
using ECommerceAPII.Domain.Entities;
using ECommerceAPII.Domain.Entities.Common;

namespace ECommerceAPII.Domain.Entities;

public class Order :BaseEntity
{
    //public Guid BasketId { get; set; } 

    public string? Description { get; set; }

    public string? Address { get; set; }

    //public Guid CustomerId { get; set; }

    public string OrderCode { get; set; }

    public Basket Basket { get; set; }

    //public ICollection<Product> Products { get; set; }

    //public Customer Customer { get; set; }

    public CompletedOrder CompletedOrder { get; set; }
}

