using System.Net;

namespace OneOf.WebApi.Middlewares;

public class MiddlewareWithInterface: IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NoContent;
            await context.Response.WriteAsync(ex.Message); // Custom Model
        }
    }
}