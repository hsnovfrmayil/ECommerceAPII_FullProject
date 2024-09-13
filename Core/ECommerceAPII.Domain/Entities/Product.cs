using System;
using ECommerceAPII.Domain.Entities.Common;
using ECommerceAPII.Domain.Entities ;

namespace ECommerceAPII.Domain.Entities;

public class Product :BaseEntity
{
    public string? Name { get; set; }

    public int Stock { get; set; }

    public long Price { get; set; }

    //public ICollection<Order> Orders { get; set; }

    public ICollection<BasketItem> BasketItems { get; set; }
}

