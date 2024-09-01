using System;
using ECommerceAPII.Application.Repositories;
using MediatR;

namespace ECommerceAPII.Application.Features.Queries.Product.GetByIdProduct;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
    readonly IProductReadRepository _productReadRepository;

    public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {
        ECommerceAPII.Domain.Entities.Product product= await _productReadRepository.GetByIdAsync(request.Id, false);
        return new()
        {
            Name=product.Name,
            Stock=product.Stock,
            Price=product.Price
        };
    }
    
}

