using System;
using ECommerceAPII.Application.Consts;
using ECommerceAPII.Application.CustomAttributes;
using ECommerceAPII.Application.Enums;
using ECommerceAPII.Application.Features.Commands.Order.CompletedOrder;
using ECommerceAPII.Application.Features.Commands.Order.CreateOrder;
using ECommerceAPII.Application.Features.Queries.Order.GetAllOrders;
using ECommerceAPII.Application.Features.Queries.Order.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPII.API.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes ="Admin")]
public class OrdersController :ControllerBase
{
    readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{Id}")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order By Id")]
    public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
    {
        GetOrderByIdQueryResponse response = await _mediator.Send(getOrderByIdQueryRequest);

        return Ok(response);
    }

     [HttpGet]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order")]
    public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
    {
        GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);

        return Ok(response);
    }

    [HttpPost]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Writing, Definition = "Add Order")]
    public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
    {
        CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);

        return Ok(response);
    }

    [HttpGet("completed-order/{Id}")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Completed Order")]
    public async Task<IActionResult> CompletedOrder([FromRoute]CompletedOrderCommandRequest completedOrderCommandRequest)
    {
        CompletedOrderCommandResponse response = await _mediator.Send(completedOrderCommandRequest);
        return Ok(response);
    }
}

