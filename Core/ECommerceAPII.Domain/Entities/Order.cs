using System;
using ECommerceAPII.Domain.Entities;
using ECommerceAPII.Domain.Entities.Common;

namespace ECommerceAPII.Domain.Entities;

public class Order :BaseEntity
{
    public string? Description { get; set; }

    public string? Address { get; set; }

    public Guid CustomerId { get; set; }

    public ICollection<Product> Products { get; set; }

    public Customer Customer { get; set; }
}

