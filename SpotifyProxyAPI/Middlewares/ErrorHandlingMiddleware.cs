using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;
using SpotifyProxyAPI.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SpotifyProxyAPI.Middlewares
{
    /// <summary>
    /// Middleware which throws custom error response if there are any exceptions in the application
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// To process HTTP request
        /// </summary>
        private readonly RequestDelegate _next;

    
        /// <summary>
        /// Custom parameter constructor
        /// </summary>
        /// <param name="requestDelegate"></param>
        public ErrorHandlingMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        /// <summary>
        /// Asynchronus method which checks for any exception in the context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
      {
            try
            {
                await _next(context);
                
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Asynchronus method which writes custom error response to the response body
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new ErrorResponse
            {
                StatusCode = 500,
                ErrorMessage = "Internal Server Error"
            });
            await context.Response.WriteAsync(result);
        }
    }
}
