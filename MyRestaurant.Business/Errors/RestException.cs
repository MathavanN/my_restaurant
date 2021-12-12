using System.Net;
using System.Runtime.Serialization;

namespace MyRestaurant.Business.Errors
{
    public class RestException : Exception, ISerializable
    {
        public HttpStatusCode ErrorCode { get; }
        public string ErrorType { get; }
        public string ErrorMessage { get; }

        public RestException(HttpStatusCode statusCode, string message) : base(message)
        {
            ErrorCode = statusCode;
            ErrorType = statusCode.ToString();
            ErrorMessage = message;
        }
    }
}
