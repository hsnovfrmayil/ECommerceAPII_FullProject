using System;
using System.Net;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Application.RequestParameters;
using ECommerceAPII.Application.Services;
using ECommerceAPII.Application.ViewModels.Products;
using ECommerceAPII.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPII.API.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductController:ControllerBase
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    readonly IFileService _fileService;
    readonly IFileWriteRepository _fileWriteRepository;
    readonly IFileReadRepository _fileReadRepository;
    readonly IProductImageFileReadRepository _productImageFileReadRepository;
    readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
    readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;

    public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository,
        IWebHostEnvironment webHostEnvironment, IFileService fileService,
        IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository,
        IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository
        )
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _webHostEnvironment = webHostEnvironment;
        _fileService = fileService;
        _fileWriteRepository = fileWriteRepository;
        _fileReadRepository = fileReadRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _invoiceFileReadRepository = invoiceFileReadRepository;
        _invoiceFileWriteRepository = invoiceFileWriteRepository;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> Get([FromQuery]Pagination pagination)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();
        var products= _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size)
            .Select(p => new
        {
            p.Id,
            p.Name,
            p.Stock,
            p.Price,
            p.CreatedTime
        });

        return Ok(new{ totalCount, products });
    }
    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await  _productReadRepository.GetByIdAsync(id,false));
    }

    [HttpPost("AddProduct")]
    public async Task<IActionResult> Post(VM_Create_Product model)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name=model.Name,
            Stock=model.Stock,
            Price=model.Price
        });

        await _productWriteRepository.SaveAsync();
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut("PutProduct")]
    public async Task<IActionResult> Put(VM_Update_Product model)
    {
        Product product =await _productReadRepository.GetByIdAsync(model.Id);
        product.Stock = model.Stock;
        product.Name = model.Name;
        product.Price = model.Price;

        await _productWriteRepository.SaveAsync();
        return Ok();  
    }

    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _productWriteRepository.RemoveAsync(id);
        await _productWriteRepository.SaveAsync();
        return Ok();
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Upload()
    {
        var datas= await _fileService.UploadAsync("resource/product-images",Request.Form.Files);
       await  _productImageFileWriteRepository.AddRangeAsync(datas.Select(d=>new ProductImageFile() {
           FileName=d.fileName,
           Path=d.path
       }).ToList());

        await _productImageFileWriteRepository.SaveAsync();
        return Ok();
    }
}

