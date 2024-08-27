using System;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Persistence.Contexts;

namespace ECommerceAPII.Persistence.Repositories;

public class FileWriteRepository : WriteRepository<ECommerceAPII.Domain.Entities.File>, IFileWriteRepository
{
    public FileWriteRepository(ECommerceAPIIDbContext context) : base(context)
    {
    }
}

