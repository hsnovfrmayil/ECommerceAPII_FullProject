using System.Net;
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
[Authorize(AuthenticationSchemes="Admin")]
public class ProductController:ControllerBase
{
    readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator; 
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
    public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
    {
        CreateProductCommandResponse response= await _mediator.Send(createProductCommandRequest);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut("PutProduct")]
    public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
    {
        UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
        return Ok();  
    }

    [HttpDelete("DeleteProduct/{Id}")]
    public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
    {
        RemoveProductCommandResponse response=await _mediator.Send(removeProductCommandRequest);
        return Ok();
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Upload([FromQuery,FromForm] UploadProductImageCommandRequest uploadProductImageCommandRequest)
    {
        UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
        return Ok();
    }
}

