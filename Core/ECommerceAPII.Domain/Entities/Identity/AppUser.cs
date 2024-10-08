﻿using System;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPII.Domain.Entities.Identity;

public class AppUser :IdentityUser<string>
{
    public string NameSurname { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenEndTime  { get; set; }

    public ICollection<Basket> Baskets { get; set; }
}

