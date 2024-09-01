using System;
using ECommerceAPII.Application.Abstractions.Storage;
using ECommerceAPII.Application.Repositories;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.ProductImageFile.UploadProductImage;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
{
    readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    readonly IStorageService _storageService;

    public UploadProductImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService)
    {
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _storageService = storageService;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        var datas = await _storageService.UploadAsync("resource/files", request.Files);
        // var datas= await _fileService.UploadAsync("resource/product-images",Request.Form.Files);
        await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ECommerceAPII.Domain.Entities.ProductImageFile()
        {
            FileName = d.fileName,
            Path = d.pathOrContainerName,
            Storage = _storageService.StorageName
        }).ToList());

        await _productImageFileWriteRepository.SaveAsync();
        return new();
    }
}

