using System;
using ECommerceAPII.Domain.Entities.Common;
using ECommerceAPII.Domain.Entities;

namespace ECommerceAPII.Domain.Entities;

public class Customer :BaseEntity
{
    public string Name { get; set; }

    //public ICollection<Order> Orders { get; set; }
}

