using System;
namespace ECommerceAPII.Application.DTOs;

public class Token
{
    public string AccessToken { get; set; }

    public DateTime Expiration { get; set; }

    //refreshToken ucun sonradan add edilir asagi
    public string RefreshToken { get; set; }
}

