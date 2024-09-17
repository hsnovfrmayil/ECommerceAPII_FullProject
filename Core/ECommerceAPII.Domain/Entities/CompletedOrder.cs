using System;
using ECommerceAPII.Domain.Entities.Common;

namespace ECommerceAPII.Domain.Entities;

public class CompletedOrder :BaseEntity
{
    public Guid OrderId { get; set; }

    public Order Order { get; set; }
}

