using Microsoft.AspNetCore.Diagnostics;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using System.Net;

namespace Postech8SOAT.FastOrder.WebAPI.Middlewares;

public static class ApiExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(
            appError => appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                var statusCode = contextFeature?.Error is DomainExceptionValidation ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.InternalServerError;

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message,
                    }.ToString());

                    return;
                }
            })
      );
    }
}
