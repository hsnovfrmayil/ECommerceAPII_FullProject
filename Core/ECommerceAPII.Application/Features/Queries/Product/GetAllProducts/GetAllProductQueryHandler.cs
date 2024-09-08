using System;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Application.RequestParameters;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPII.Application.Features.Queries.Product.GetAllProducts;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    readonly ILogger<GetAllProductQueryHandler> _logger;

    public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
    {
        _productReadRepository = productReadRepository;
        _logger = logger;
    }

    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("get all products");
        //throw new Exception("hata alindi yaxsi oglan");
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

