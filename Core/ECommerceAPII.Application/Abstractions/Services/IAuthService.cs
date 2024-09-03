using System;
using ECommerceAPII.Application.Abstractions.Services.Authentications;

namespace ECommerceAPII.Application.Abstractions.Services;

public interface IAuthService : IInternalAuthentication, IExternalAuthentication
{

}

