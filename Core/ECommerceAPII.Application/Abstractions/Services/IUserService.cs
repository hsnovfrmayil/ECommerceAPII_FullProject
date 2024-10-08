﻿using System;
using ECommerceAPII.Application.DTOs.User;
using ECommerceAPII.Domain.Entities.Identity;

namespace ECommerceAPII.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse> CreateAsync(CreateUser model);

    Task UpdateRefreshTokenAsync(string refreshToken,AppUser user,DateTime accessTokenDate,int addOnAccessTokenDate);

    Task UpdatePasswordAsync(string userId,string resetToken,string newPassword);
}

