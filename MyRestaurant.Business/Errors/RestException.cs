using System.Net;
using System.Runtime.Serialization;

namespace MyRestaurant.Business.Errors
{
    [Serializable]
    public class RestException : Exception, ISerializable
    {
        [NonSerialized]
        private readonly HttpStatusCode _statusCode;
        [NonSerialized]
        private readonly string _code;
        [NonSerialized]
        private readonly string _errorMessage;

        public RestException(HttpStatusCode statusCode, string code, string message) : base(message)
        {
            _statusCode = statusCode;
            _code = code;
            _errorMessage = message;
        }

        protected RestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _statusCode = (HttpStatusCode)info.GetValue("StatusCode", typeof(HttpStatusCode))!;
            _code = info.GetString("Code")!;
            _errorMessage = info.GetString("ErrorMessage")!;
        }

        public HttpStatusCode StatusCode { get { return _statusCode; } }
        public string Code { get { return _code; } }
        public string ErrorMessage { get { return _errorMessage; } }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", _code);
            info.AddValue("ErrorMessage", _errorMessage);
            info.AddValue("StatusCode", _statusCode, typeof(HttpStatusCode));
            
            // MUST call through to the base class to let it save its own state
            base.GetObjectData(info, context);
        }
    }
}
