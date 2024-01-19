using CryptographyAes.WebApi.Entities.Dto;

namespace CryptographyAes.WebApi.Entities.Exceptions;

public class XException : Exception
{
    public DefaultResponse? defaultResponse { get; set; }

    public XException()
    {
    }

    public XException(string message) : base(message)
    {
        defaultResponse = new DefaultResponse()
        {
            status = false,
            statusDescription = $"Error {message}",
            data = ""
        };
    }
}

