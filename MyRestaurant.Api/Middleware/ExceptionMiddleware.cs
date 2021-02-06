using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MyRestaurant.Business.Errors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;

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
            object error = null;

            switch (ex)
            {
                case RestException re:
                    error = new { re.ErrorCode, re.ErrorType, re.ErrorMessage, ErrorDate = DateTime.Now };
                    logger.LogError("REST ERROR: {0}", error);
                    context.Response.StatusCode = (int)re.ErrorCode;
                    break;

                case Exception e:

                    var receiveException = e.InnerException ?? e;
                    var sqlError = receiveException as SqlException;
                    if (sqlError.Number == 547)
                    {
                        logger.LogError("SERVER ERROR: {0}", receiveException.Message);
                        error = new
                        {
                            ErrorCode = HttpStatusCode.Conflict,
                            ErrorType = HttpStatusCode.Conflict.ToString(),
                            ErrorMessage = "This item cannot be deleted.",
                            ErrorDate = DateTime.Now
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    }
                    else
                    {
                        logger.LogError("SERVER ERROR: {0}", receiveException.Message);
                        error = new
                        {
                            ErrorCode = HttpStatusCode.InternalServerError,
                            ErrorType = HttpStatusCode.InternalServerError.ToString(),
                            ErrorMessage = string.IsNullOrWhiteSpace(receiveException.Message) ? "Error" : receiveException.Message,
                            ErrorDate = DateTime.Now
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }
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
