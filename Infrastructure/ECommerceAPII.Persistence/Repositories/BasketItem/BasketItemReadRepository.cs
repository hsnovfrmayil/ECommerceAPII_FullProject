using System;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Domain.Entities;
using ECommerceAPII.Persistence.Contexts;

namespace ECommerceAPII.Persistence.Repositories;

public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
{
    public BasketItemReadRepository(ECommerceAPIIDbContext context) : base(context)
    {
    }
}

