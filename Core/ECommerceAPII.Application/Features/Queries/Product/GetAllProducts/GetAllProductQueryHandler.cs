using System;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Application.RequestParameters;
using MediatR;

namespace ECommerceAPII.Application.Features.Queries.Product.GetAllProducts;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();
        var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size)
            .Take(request.Size).Select(p => new
        {
            p.Id,
            p.Name,
            p.Stock,
            p.Price,
            p.CreatedTime
        }).ToList();

        return new()
        {
            Products = products,
            TotalCount = totalCount
        };
    }
}

