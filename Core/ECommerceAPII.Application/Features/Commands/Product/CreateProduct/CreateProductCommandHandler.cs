using System;
using ECommerceAPII.Application.Abstractions.Hubs;
using ECommerceAPII.Application.Repositories;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    readonly IProductWriteRepository _productWriteRepository;
    readonly IProductHubService _productHubService;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
    {
        _productWriteRepository = productWriteRepository;
        _productHubService = productHubService;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name = request.Name,
            Stock = request.Stock,
            Price = request.Price
        });

        await _productWriteRepository.SaveAsync();
        _productHubService.ProductAddedMessageAsync($"{request.Name} adli product yaradildi!");
        return new();
    }
}

