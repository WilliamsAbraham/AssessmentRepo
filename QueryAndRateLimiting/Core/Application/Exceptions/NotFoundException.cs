using System.Net;

namespace CustomerServiceApi.Core.Application.Exceptions;
public class NotFoundException : CustomException
{
    public NotFoundException(string message)
        : base(message, null, HttpStatusCode.NotFound)
    {
    }
}