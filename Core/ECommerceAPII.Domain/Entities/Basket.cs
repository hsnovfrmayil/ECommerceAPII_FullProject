using System;
using ECommerceAPII.Domain.Entities.Common;
using ECommerceAPII.Domain.Entities.Identity;

namespace ECommerceAPII.Domain.Entities;

public class Basket :BaseEntity
{
    public string AppUserId { get; set; }

    public AppUser User { get; set; }

    public Order Order { get; set; }

    public ICollection<BasketItem> BasketItems { get; set; }
}

