using Microsoft.AspNetCore.Http;
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
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ExceptionMiddleware> logger)
        {
            object errors = null;

            switch (ex)
            {
                case RestException re:
                    errors = new { re.ErrorCode, re.ErrorType, re.ErrorMessage, ErrorDate = DateTime.Now };
                    logger.LogError("REST ERROR: {0}", errors);
                    context.Response.StatusCode = (int)re.ErrorCode;
                    break;

                case Exception e:
                    logger.LogError("SERVER ERROR: {0}", e.Message);
                    errors = new { ErrorCode= HttpStatusCode.InternalServerError, 
                        ErrorType= HttpStatusCode.InternalServerError.ToString(), 
                        ErrorMessage= string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message, 
                        ErrorDate = DateTime.Now };
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if(errors != null )
            {
                var result = JsonConvert.SerializeObject(new { errors }, Formatting.Indented, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
