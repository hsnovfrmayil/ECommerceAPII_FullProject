using System;
using ECommerceAPI.Persistence;
using ECommerceAPII.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPII.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIIDbContext>
{
    public ECommerceAPIIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ECommerceAPIIDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString());

        return new(dbContextOptionsBuilder.Options);
    }
}

