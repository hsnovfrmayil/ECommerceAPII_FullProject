using System;
namespace ECommerceAPII.Application.Exceptions;

public class PasswordChangeFailedException :Exception
{
    public PasswordChangeFailedException() : base("Sifre update edilerken bir xeta yarandi...")
    {
    }

    public PasswordChangeFailedException(string? message) : base(message)
    {
    }

    public PasswordChangeFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

