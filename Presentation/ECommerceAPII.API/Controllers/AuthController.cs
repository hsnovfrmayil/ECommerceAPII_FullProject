using System;
using ECommerceAPII.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPII.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPII.Application.Features.Commands.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPII.API.Controllers;

public class AuthController :ControllerBase
{
     readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]LoginUserCommandRequest loginUserCommandRequest)
    {
        LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
        return Ok(response);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> RefreshTokenLogin([FromForm]RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
    {
        RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
        return Ok(response);
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
    {
        GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
        return Ok(response);
    }

    //[HttpPost("facebook-login")]
    //public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
    //{
    //    FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
    //    return Ok(response);
    //}
}

