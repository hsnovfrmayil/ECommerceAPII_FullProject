using System;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Domain.Entities;
using ECommerceAPII.Persistence.Contexts;

namespace ECommerceAPII.Persistence.Repositories;

public class FileReadRepository : ReadRepository<ECommerceAPII.Domain.Entities.File>, IFileReadRepository
{
    public FileReadRepository(ECommerceAPIIDbContext context) : base(context)
    {
    }
}

