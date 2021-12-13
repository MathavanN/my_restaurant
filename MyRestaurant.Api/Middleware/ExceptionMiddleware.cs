using Microsoft.Data.SqlClient;
using MyRestaurant.Business.Errors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace MyRestaurant.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ExceptionMiddleware> logger)
        {
            object error = new();

            switch (ex)
            {
                case RestException re:
                    error = new { re.ErrorCode, re.ErrorType, re.ErrorMessage, ErrorDate = DateTime.Now };
                    logger.LogError("REST ERROR: {error}. {ex}", error, ex);
                    context.Response.StatusCode = (int)re.ErrorCode;
                    break;

                case Exception e:
                    var receivedException = e.InnerException ?? e;
                    logger.LogError("SERVER ERROR: {receivedException.Message}. {ex}", receivedException.Message, ex);
                    var isSqlError = receivedException is SqlException;
                    var sqlError = receivedException as SqlException;
                    var errorCode = HttpStatusCode.InternalServerError;
                    var errorType = HttpStatusCode.InternalServerError.ToString();
                    var errorMessage = HttpStatusCode.InternalServerError.ToString();

                    if (!string.IsNullOrWhiteSpace(receivedException.Message))
                        errorMessage = receivedException.Message;

                    if (isSqlError && sqlError!.Number == 547)
                    {
                        errorCode = HttpStatusCode.Conflict;
                        errorType = HttpStatusCode.Conflict.ToString();
                        errorMessage = "This item cannot be deleted.";
                        context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    }
                    else
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    error = new { ErrorCode = errorCode, ErrorType = errorType, ErrorMessage = errorMessage, ErrorDate = DateTime.Now };
                    break;
            }

            context.Response.ContentType = "application/json";
            if (error != null)
            {
                var result = JsonConvert.SerializeObject(error, Formatting.Indented, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
