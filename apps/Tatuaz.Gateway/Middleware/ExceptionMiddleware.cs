using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Tatuaz.Shared.Pipeline.Factories.Results;

namespace Tatuaz.Gateway.Middleware;

/// <summary>
/// Main middleware for handling exceptions. This application does not use
/// exception based error handling, so every exception will result in a 500
/// and a generic error message.
/// </summary>
public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    /// <summary>
    /// Default controller
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    /// <summary>
    /// Try to execute rest of the pipeline. If an exception is thrown, handle it.
    /// Every exception at this point is a internal server error.
    /// </summary>
    /// <param name="httpContext"></param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response
            .WriteAsJsonAsync(
                HttpHelpers.ToErrorsObject(CommonResultFactory.InternalError<object>().Errors)
            )
            .ConfigureAwait(false);

        if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, "Internal server error:");
        }
    }
}
