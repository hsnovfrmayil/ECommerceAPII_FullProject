using System;
namespace ECommerceAPII.Application.Exceptions;

public class NotFoundUserException :Exception
{
	public NotFoundUserException():base("istifadeci adi ve ya sifresi yanlisdir...")
	{
	}

    public NotFoundUserException(string? message) : base(message)
    {
    }

    public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

