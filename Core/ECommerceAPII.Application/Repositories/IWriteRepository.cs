using System;
using ECommerceAPII.Domain.Entities.Common;

namespace ECommerceAPII.Application.Repositories;

public interface IWriteRepository<T> :IRepository<T> where T : BaseEntity
{
    Task<bool> AddAsync(T model);

    Task<bool> AddRangeAsync(List<T> datas);

    bool Remove(T model);

    bool RemoveRange(List<T> datas);

    Task<bool> RemoveAsync(string id);

    bool UpdateAsync(T model);

    Task<int> SaveAsync();
}

