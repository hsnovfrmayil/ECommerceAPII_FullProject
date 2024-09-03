using System;
using MediatR;

namespace ECommerceAPII.Application.Features.Commands.RefreshTokenLogin;

public class RefreshTokenLoginCommandRequest :IRequest<RefreshTokenLoginCommandResponse>
{
    public string RefreshToken { get; set; }
}

