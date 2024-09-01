using System;
namespace ECommerceAPII.Application.Abstractions.Storage;

public interface IStorageService :IStorage
{
    public string StorageName { get; }
}

