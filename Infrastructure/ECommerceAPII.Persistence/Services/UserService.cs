using System;
using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.DTOs.User;
using ECommerceAPII.Application.Exceptions;
using ECommerceAPII.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPII.Application.Helpers;
using ECommerceAPII.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Path = ECommerceAPII.Domain.Entities.Identity;

namespace ECommerceAPII.Persistence.Services;

public class UserService : IUserService
{
    readonly UserManager<Path.AppUser> _userManager;

    public UserService(UserManager<Path.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponse> CreateAsync(CreateUser model)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = model.Username,
            Email = model.Email,
            NameSurname = model.NameSurname
        }, model.Password);

        CreateUserResponse response = new() { Succeeded = result.Succeeded };

        if (result.Succeeded)
            response.Message = "Istifadeci ugurla yaradildi!";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code}-{error.Description}<br>";

        return response;
    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
    {
        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndTime = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(user);
        }
        else
            throw new NotFoundUserException();
    }

    public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
    {
        AppUser user=await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            resetToken = resetToken.UrlDecode();

            IdentityResult result = await _userManager.ResetPasswordAsync(user,resetToken,newPassword);
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                throw new PasswordChangeFailedException();
        }
    }

}

