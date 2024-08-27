using System;
using System.Linq.Expressions;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Domain.Entities.Common;
using ECommerceAPII.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPII.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ECommerceAPIIDbContext _context;

    public ReadRepository(ECommerceAPIIDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query=query.AsNoTracking();

        return query; 
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(method);
    }

    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    //=> await Table.FirstOrDefaultAsync(data=>data.Id==Guid.Parse(id));
    //=> await Table.FindAsync(Guid.Parse(id)); {
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(data=>data.Id==Guid.Parse(id));
    }
       
    
}

