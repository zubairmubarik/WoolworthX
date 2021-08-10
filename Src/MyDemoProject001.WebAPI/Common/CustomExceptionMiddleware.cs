using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyDemoProject001.WebAPI.Model;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyDemoProject001.WebAPI.Common
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(ILogger<CustomExceptionMiddleware> logger, RequestDelegate requestDelegate)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                httpContext.Request.EnableBuffering();

                // Leave the body open so the next middleware can read it.
                using (var reader = new StreamReader(
                    httpContext.Request.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: 1024*100,
                    leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();                    

                    _logger.LogInformation($"Testing: {DateTime.Now.Minute} Http Response Information:{Environment.NewLine}" +
                           $"Schema:{body} " +
                           $"Host: {httpContext.Request.Host} " +
                           $"Method: {httpContext.Request.Method} " +
                           $"Path: {httpContext.Request.Path} " +
                           $"QueryString: {httpContext.Request.QueryString} " +
                           $"Response Body: {body}");
                    // Do some processing with body…

                    // Reset the request body stream position so the next middleware can read it
                    httpContext.Request.Body.Position = 0;
                }

                

                // Call the next delegate/middleware in the pipeline


                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Exception: {ex}");
                await HandleExceptionAsync(httpContext);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error from DempPrpoject Middleware"
            }.ToString());

        }
    }
}
