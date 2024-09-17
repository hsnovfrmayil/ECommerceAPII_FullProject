using System;
using ECommerceAPII.Application.Enums;

namespace ECommerceAPII.Application.CustomAttributes;

public class AuthorizeDefinitionAttribute :Attribute
{
    public string Menu { get; set; }

    public string Definition { get; set; }

    public ActionType ActionType { get; set; }
}

