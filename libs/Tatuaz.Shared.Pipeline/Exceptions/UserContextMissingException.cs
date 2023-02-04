using System;

namespace Tatuaz.Shared.Pipeline.Exceptions;

public class UserContextMissingException : Exception
{
    public UserContextMissingException() : base("User context is missing") { }
}
