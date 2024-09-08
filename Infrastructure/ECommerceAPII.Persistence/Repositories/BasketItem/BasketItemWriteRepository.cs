using System;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Domain.Entities;
using ECommerceAPII.Persistence.Contexts;

namespace ECommerceAPII.Persistence.Repositories;

public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
{
    public BasketItemWriteRepository(ECommerceAPIIDbContext context) : base(context)
    {
    }
}

