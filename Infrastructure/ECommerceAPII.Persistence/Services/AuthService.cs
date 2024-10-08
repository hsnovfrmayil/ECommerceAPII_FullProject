﻿using System;
using System.Text;
using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.Abstractions.Token;
using ECommerceAPII.Application.DTOs;
using ECommerceAPII.Application.Exceptions;
using ECommerceAPII.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPII.Application.Helpers;
using ECommerceAPII.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Path = ECommerceAPII.Domain.Entities.Identity;

namespace ECommerceAPII.Persistence.Services;

public class AuthService : IAuthService
{
    readonly IConfiguration _configuration;
    readonly UserManager<Path.AppUser> _userManager;
    readonly ITokenHandler _tokenHandler;
    readonly SignInManager<Path.AppUser> _signInManager;
    readonly IUserService _userService;
    readonly IMailService _mailService;

    public AuthService(IConfiguration configuration, UserManager<Path.AppUser>  userManager, ITokenHandler tokenHandler,
        SignInManager<Path.AppUser> signInManager, IUserService userService, IMailService mailService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
        _userService = userService;
        _mailService = mailService;
    }

    async Task<Token> CreateUserExternalAsync(AppUser user,string email,string name,UserLoginInfo info,int accessTokenLifeTime)
    {
        bool result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = email,
                    NameSurname = name
                };
                var identitResult = await _userManager.CreateAsync(user);
                result = identitResult.Succeeded;
            }
        }
        if (result)
        {
            await _userManager.AddLoginAsync(user,info);

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime,user);

            //refresh arasi
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken,user,token.Expiration,10);


            // eynisin logine falanda elave edeceyik
            return token;
        }
        throw new Exception("Invalid external authentication...");
    }


    public async Task<Token> GoogleLoginAsync(string idToken,int accessTokenLifeTime)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
        Path.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        return await CreateUserExternalAsync(user,payload.Email,payload.Name,info,accessTokenLifeTime);
    }

    public async Task<Token> LoginAsync(string usernameOrEmail, string password,int accessTokenLifeTime)
    {
        Path.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);

        if (user == null)
            user = await _userManager.FindByEmailAsync(usernameOrEmail);

        if (user == null)
            throw new NotFoundUserException();

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,password, false);

        if (result.Succeeded)
        {
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime,user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);
            return token;
        }

        throw new AuthenticationErrorException();
    }

    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user= _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);

        if (user != null && user?.RefreshTokenEndTime>DateTime.UtcNow)
        {
            Token token = _tokenHandler.CreateAccessToken(15,user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
            return token;
        }
        else
            throw new NotFoundUserException();
    }

    public async Task PasswordResetAsync(string email)
    {
        AppUser user= await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            string resetToken= await _userManager.GeneratePasswordResetTokenAsync(user);
            //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
            //resetToken= WebEncoders.Base64UrlEncode(tokenBytes);
            resetToken=resetToken.UrlEncode();
            await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken); 
        }
    }

    public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
    {
        AppUser user=await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
            //resetToken= Encoding.UTF8.GetString(tokenBytes);
            resetToken=resetToken.UrlDecode();

            return await _userManager.VerifyUserTokenAsync(user,_userManager.Options.Tokens.PasswordResetTokenProvider,"ResetPassword",resetToken);
        }
        return false;
    }
}

