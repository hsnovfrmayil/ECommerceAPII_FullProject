using System;
using ECommerceAPII.Application.ViewModels.Baskets;
using ECommerceAPII.Domain.Entities;

namespace ECommerceAPII.Application.Abstractions.Services;

public interface  IBasketService
{
    public Task<List<BasketItem>> GetBasketItemsasync();

    public Task AddItemToBasketAsync(VM_Create_BasketItem basketItem);

    public Task UpdateQuantityAsync(VM_Create_BasketItem basketItem);

    public Task RemoveBasketItemAsync(string basketItemId); 
}

