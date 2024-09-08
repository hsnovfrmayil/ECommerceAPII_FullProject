using System;
using ECommerceAPII.Domain.Entities.Identity;

namespace ECommerceAPII.Application.Abstractions.Token;

public interface ITokenHandler
{
    DTOs.Token CreateAccessToken(int second,AppUser appUser);

    string CreateRefreshToken();
}

