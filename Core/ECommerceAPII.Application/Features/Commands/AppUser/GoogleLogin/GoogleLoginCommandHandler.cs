using System;
using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.Abstractions.Token;
using ECommerceAPII.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPII.Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    readonly IAuthService _authService;

    public GoogleLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var token= await _authService.GoogleLoginAsync(request.IdToken,15);

        return new()
        {
            Token = token
        };
    }
}

