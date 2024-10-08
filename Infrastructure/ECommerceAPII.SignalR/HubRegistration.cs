﻿using System;
using ECommerceAPII.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ECommerceAPII.SignalR;

public static class HubRegistration
{
    public static void MapHubs(this WebApplication webApplication)
    {
        webApplication.MapHub<ProductHub>("/products-hub");
        webApplication.MapHub<ProductHub>("/orders-hub");
    }
}

