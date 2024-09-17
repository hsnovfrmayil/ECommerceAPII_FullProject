using System;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Domain.Entities;
using ECommerceAPII.Persistence.Contexts;

namespace ECommerceAPII.Persistence.Repositories;

public class CompletedOrderReadRepository : ReadRepository<CompletedOrder>, ICompletedOrderReadRepository
{
    public CompletedOrderReadRepository(ECommerceAPIIDbContext context) : base(context)
    {
    }
}

