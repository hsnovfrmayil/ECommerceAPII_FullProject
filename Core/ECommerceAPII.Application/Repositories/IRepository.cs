using System;
namespace ECommerceAPII.Application.Repositories;

using ECommerceAPII.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;


public interface IRepository<T> where T: BaseEntity
{
    DbSet<T> Table { get; }
}

