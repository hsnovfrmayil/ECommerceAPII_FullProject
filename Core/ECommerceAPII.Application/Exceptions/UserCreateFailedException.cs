using System;
using System.Runtime.Serialization;

namespace ECommerceAPII.Application.Exceptions;

public class UserCreateFailedException : Exception
{
    public UserCreateFailedException():base("istifadeci yaradilirken gozlenilmeyen xeta ile qarsilasildi...")
    {
    }

    public UserCreateFailedException(string? message,Exception? innerException) : base(message, innerException)
    {
    }
}

