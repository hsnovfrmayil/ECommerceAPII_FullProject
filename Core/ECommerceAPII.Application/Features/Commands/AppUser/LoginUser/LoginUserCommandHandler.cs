using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.Abstractions.Token;
using ECommerceAPII.Application.DTOs;
using ECommerceAPII.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Path = ECommerceAPII.Domain.Entities.Identity;

namespace ECommerceAPII.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    readonly IAuthService _authService;

    public LoginUserCommandHandler(IAuthService authService)
    {
        _authService = authService; 
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var token= await _authService.LoginAsync(request.UsernameOrEmail,request.Password,15);

        return new LoginUserCommandSuccessResponse()
        {
            Token = token
        };
    }
}

