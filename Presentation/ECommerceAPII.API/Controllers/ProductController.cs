using System.Net;
using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPII.Application.Features.Commands.Product.RemoveProduct;
using ECommerceAPII.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPII.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ECommerceAPII.Application.Features.Queries.Product.GetAllProducts;
using ECommerceAPII.Application.Features.Queries.Product.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPII.API.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductController:ControllerBase
{
    readonly IMediator _mediator;
    readonly IMailService _mailService;

    public ProductController(IMediator mediator, IMailService mailService)
    {
        _mediator = mediator;
        _mailService = mailService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> Get([FromQuery]GetAllProductQueryRequest getAllProductQueryRequest)
    {
        GetAllProductQueryResponse response= await _mediator.Send(getAllProductQueryRequest);
        return Ok(response);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
    {
        GetByIdProductQueryResponse response=await _mediator.Send(getByIdProductQueryRequest);
        return Ok(response);
    }

    [HttpPost("AddProduct")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
    {
        CreateProductCommandResponse response= await _mediator.Send(createProductCommandRequest);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut("PutProduct")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
    {
        UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
        return Ok();  
    }

    [HttpDelete("DeleteProduct/{Id}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
    {
        RemoveProductCommandResponse response=await _mediator.Send(removeProductCommandRequest);
        return Ok();
    }


    [HttpPost("[action]")]
    [Authorize(AuthenticationSchemes = "Admin")] 
    public async Task<IActionResult> Upload([FromQuery,FromForm] UploadProductImageCommandRequest uploadProductImageCommandRequest)
    {
        UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> ExampleMailTest()
    {
        await _mailService.SendMessageAsync("hesenovfermayil765@gmail.com", "Example Mail", "<h1>Example Test Mail</h1>");
        return Ok();
    }
}

