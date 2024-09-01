using System;
using ECommerceAPII.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Path = ECommerceAPII.Domain.Entities.Identity;

namespace ECommerceAPII.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    readonly UserManager<Path.AppUser> _userManager;
    readonly SignInManager<Path.AppUser> _signInManager;

    public LoginUserCommandHandler(UserManager<Path.AppUser> userManager, SignInManager<Path.AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        Path.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        if (user == null)
            throw new NotFoundUserException();

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,request.Password,false);

        if (result.Succeeded)
        {

        }

        return new();
    }
}

