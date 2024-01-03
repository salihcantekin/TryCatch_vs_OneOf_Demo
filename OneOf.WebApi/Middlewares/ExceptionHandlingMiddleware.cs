using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace OneOf.WebApi.Middlewares;

public static class ExceptionHandlingMiddleware
{
    public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async (context) =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                context.Response.StatusCode = (int)HttpStatusCode.Ambiguous;
                context.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Plain;

                if (feature?.Error is Exception ex) // Custom Exception (Validation)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    await context.Response.WriteAsync(ex.Message); // Custom Model
                }
                else if (feature?.Error is Exception otherEx)
                {
                    // doSomething()
                }
            });
        });

        return app;
    }
}
