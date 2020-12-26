using System;
using System.Net;

namespace MyRestaurant.Business.Errors
{
    public class RestException : Exception
    {
        public HttpStatusCode ErrorCode { get; }
        public string ErrorType { get; }
        public string ErrorMessage { get; }

        public RestException(HttpStatusCode statusCode, string message)
        {
            ErrorCode = statusCode;
            ErrorType = statusCode.ToString();
            ErrorMessage = message;
        }
    }
}
