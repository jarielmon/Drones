using Drones.Domain.Common;

namespace Drones.Domain.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException() : base(HttpStatusCode.Conflict)
    {
    }

    public ConflictException(string message) : base(HttpStatusCode.Conflict, message)
    {
    }
}
