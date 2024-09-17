using System;
using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.Exceptions;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.AppUser.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
{
    readonly IUserService _userService;

    public UpdatePasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (!request.Password.Equals(request.PasswordConfirm))
            throw new PasswordChangeFailedException("Zehmet olmasa sifreni dogrulayin...");

        await _userService.UpdatePasswordAsync(request.UserId,request.ResetToken,request.Password);

        return new();
    }
}

