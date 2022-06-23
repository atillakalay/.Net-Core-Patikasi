using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        ILoggerService _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {

                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                _logger.Write(message);
                await _next(context);

                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responsed " + context.Response.StatusCode + " in " + watch.Elapsed.Milliseconds + " as ";
                _logger.Write(message);
            }
            catch (Exception exception)
            {
                watch.Stop();
                await HandleException(context, exception, watch);
            }

        }

        private Task HandleException(HttpContext context, Exception exception, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message: " + exception.Message + " in " + watch.Elapsed.TotalMilliseconds + " as ";
            _logger.Write(message);
            var result = JsonConvert.SerializeObject(new { error = exception.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionBuilder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
