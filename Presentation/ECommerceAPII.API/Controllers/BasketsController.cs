using System;
using ECommerceAPII.Application.Consts;
using ECommerceAPII.Application.CustomAttributes;
using ECommerceAPII.Application.Enums;
using ECommerceAPII.Application.Features.Commands.Basket.AddItemToBasket;
using ECommerceAPII.Application.Features.Commands.Basket.RemoveBasketItem;
using ECommerceAPII.Application.Features.Commands.Basket.UpdateQuantity;
using ECommerceAPII.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPII.API.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes ="Admin")] 
public class BasketsController :ControllerBase
{
    readonly IMediator _mediator;

    public BasketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets,ActionType =ActionType.Reading, Definition ="Get Basket Items")]
    public async Task<IActionResult> GetBasketItems([FromQuery]GetBasketItemsQueryRequest getBasketItemsQueryRequest)
    {
        List<GetBasketItemsQueryResponse > response = await _mediator.Send(getBasketItemsQueryRequest);
        return Ok(response);
    }

    [HttpPost]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Writing, Definition = "Add to Basket")]
    public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
    {
        AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
        return Ok(response);
    }

    [HttpPut]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Deleting, Definition = "Remove Basket Items ")]
    public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
    {
        UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
        return Ok(response); 
    }

    [HttpDelete("{BasketItemId}")]
    public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
    {
        RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);
        return Ok(response);
    }
}

