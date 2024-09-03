using System;
namespace ECommerceAPII.Application.Abstractions.Services.Authentications;

public interface IExternalAuthentication
{
    //Task FacebookLoginAsync();

    Task<DTOs.Token> GoogleLoginAsync(string idToken,int accessTokenLifeTime);
}

