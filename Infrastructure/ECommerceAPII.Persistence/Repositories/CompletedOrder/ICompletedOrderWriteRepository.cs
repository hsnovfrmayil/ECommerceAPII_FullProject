using System;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Domain.Entities;
using ECommerceAPII.Persistence.Contexts;

namespace ECommerceAPII.Persistence.Repositories;

public class CompletedOrderWriteRepository : WriteRepository<CompletedOrder>, ICompletedOrderWriteRepository
{
    public CompletedOrderWriteRepository(ECommerceAPIIDbContext context) : base(context)
    {
    }
}

