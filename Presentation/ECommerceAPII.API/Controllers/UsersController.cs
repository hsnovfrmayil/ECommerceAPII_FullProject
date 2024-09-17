using System;
using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.Consts;
using ECommerceAPII.Application.CustomAttributes;
using ECommerceAPII.Application.Enums;
using ECommerceAPII.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPII.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPII.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPII.Application.Features.Commands.AppUser.VerifyResetToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPII.API.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class UsersController : ControllerBase
{
    readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Add Product")]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
    {
        CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
        return Ok(response);
    }

    [HttpPost("update-password")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Add Product")]
    public async Task<IActionResult> UpdatePassword([FromBody]VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
    {
        VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);

        return Ok(response);
    }
}

