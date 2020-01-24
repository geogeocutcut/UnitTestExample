using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json.Linq;
namespace ApplicationApi.Exceptions
{
    public class BusinessExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BusinessExceptionMiddleWare> _logger;

        public BusinessExceptionMiddleWare(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<BusinessExceptionMiddleWare>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }


                throw new HttpStatusCodeException(500,new JObject(
                                                            new JProperty("CodeError", ex.CodeErreur.ToString()),
                                                            new JProperty("Message", ex.Message),
                                                            new JProperty("Debug", ex.InnerException)));
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BusinessExceptionMiddleWareExtensions
    {
        public static IApplicationBuilder UseBusinessExceptionMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BusinessExceptionMiddleWare>();
        }
    }
}