using System;
using ECommerceAPII.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Path = ECommerceAPII.Domain.Entities.Identity;

namespace ECommerceAPII.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    readonly UserManager<Path.AppUser> _userManager;

    public CreateUserCommandHandler(UserManager<Path .AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult result= await _userManager.CreateAsync(new()
        {
            Id=Guid.NewGuid().ToString(),
            UserName=request.Username,
            Email=request.Email,
            NameSurname=request.NameSurname
        },request.Password);

        CreateUserCommandResponse response = new() { Succeeded=result.Succeeded};

        if (result.Succeeded)
            response.Message = "Istifadeci ugurla yaradildi!";
        else
            foreach(var error in result.Errors)
                response.Message += $"{error.Code}-{error.Description}<br>";

        return response;

        //throw new UserCreateFailedException();
    }
}

