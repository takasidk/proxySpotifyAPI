using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;
using SpotifyProxyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpotifyProxyAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly Logger _logger;

        public static Logger Logger { get; set; }

        public ErrorHandlingMiddleware(RequestDelegate requestDelegate, IOptions<UserSettings> config)
        {
            _logger = new Serilog.LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.File(new CompactJsonFormatter(), config.Value.AuditLog.Path,
                rollingInterval: (RollingInterval)Enum.Parse(typeof(RollingInterval), config.Value.AuditLog.RollingInterval),
                shared: config.Value.AuditLog.Shared,
                retainedFileCountLimit: config.Value.AuditLog.RetainedFileCountLimit).CreateLogger();
            Logger = _logger;

            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorResponse()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = "Internal Server Error from the custom middleware."
            }.ToString());
        }
    }
}
