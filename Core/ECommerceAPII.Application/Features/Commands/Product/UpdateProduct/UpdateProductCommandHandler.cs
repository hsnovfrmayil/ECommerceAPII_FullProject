using System;
using ECommerceAPII.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPII.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    readonly IProductReadRepository _productReadRepository;
    readonly IProductWriteRepository _productWriteRepository;
    readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository,
         ILogger<UpdateProductCommandHandler> logger)
    { 
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _logger = logger;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        ECommerceAPII.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
        product.Stock = request.Stock;
        product.Name = request.Name;
        product.Price = request.Price;

        await _productWriteRepository.SaveAsync();
        _logger.LogInformation("Product guncellendi...");
        return new();
    }
}

