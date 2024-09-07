namespace ChallengeN5.Command.API.Architecture.Extension;

using ChallengeN5.Command.API.Architecture.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Primitives;
using System.Net;

using System.Text.Json;


/// <summary>
/// Exception midleware
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Configuration
    /// </summary>
    /// <param name="app"></param>
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                context.Request.Headers.TryGetValue("x-application", out StringValues xa);
                context.Request.Headers.TryGetValue("x-transaction_id", out StringValues xti);
                context.Request.Headers.TryGetValue("x-timestamp", out StringValues xts);
                context.Response.Headers.Append("x-application", xa);
                context.Response.Headers.Append("x-transaction_id", xti);
                context.Response.Headers.Append("x-timestamp", xts);

                HttpStatusCode httpCode = HttpStatusCode.InternalServerError;
                string? userFriendlyError = null;
                string? moreInformation = null;

                IExceptionHandlerFeature? contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                Exception? ex = contextFeature?.Error;

                if (ex != null)
                {
                    userFriendlyError = ex.Message;
                    moreInformation = "StackTrace:" + ex.StackTrace;

                    if (ex.GetType() == typeof(Exception))
                    {
                        httpCode = HttpStatusCode.InternalServerError;
                    }
                    else if (ex.GetType() == typeof(BadHttpRequestException))
                    {
                        httpCode = HttpStatusCode.BadRequest;
                    }
                    else if (ex.GetType() == typeof(UnauthorizedAccessException))
                    {
                        httpCode = HttpStatusCode.Unauthorized;
                    }
                    else if (ex.GetType() == typeof(KeyNotFoundException))
                    {
                        httpCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        httpCode = HttpStatusCode.InternalServerError;
                    }
                }

                context.Response.StatusCode = (int)httpCode;
                ErrorBaseResponse errorObject = new()
                {
                    HttpCode = (int)httpCode,
                    HttpMessage = Enum.GetName(typeof(HttpStatusCode), context.Response.StatusCode) ?? string.Empty,
                    InternalId = $"{xa}-{xti}",
                    UserFriendlyError = userFriendlyError ?? string.Empty,
                    MoreInformation = moreInformation ?? string.Empty
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorObject));
            });
        });
    }
}
