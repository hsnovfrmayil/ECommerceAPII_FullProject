using System;
using ECommerceAPII.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPII.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPII.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsersController :ControllerBase
{
    readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
    {
        CreateUserCommandResponse response=await _mediator.Send(createUserCommandRequest);
        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
    {
        LoginUserCommandResponse response= await _mediator.Send(loginUserCommandRequest);
        return Ok(); 
    }
}

