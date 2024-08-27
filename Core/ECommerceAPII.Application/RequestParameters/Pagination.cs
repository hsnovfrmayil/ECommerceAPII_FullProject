using System;
namespace ECommerceAPII.Application.RequestParameters;

public record Pagination
{
    public int Page { get; set; } = 1;

    public int Size { get; set; } = 5;
}

