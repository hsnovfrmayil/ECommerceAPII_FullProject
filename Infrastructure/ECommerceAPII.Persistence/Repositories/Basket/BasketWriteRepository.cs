using System;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Domain.Entities;
using ECommerceAPII.Persistence.Contexts;

namespace ECommerceAPII.Persistence.Repositories;

public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
{
    public BasketWriteRepository(ECommerceAPIIDbContext context) : base(context)
    {
    }
}

