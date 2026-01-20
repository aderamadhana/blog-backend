using System.Net;
using System.Text.Json;

namespace blog_backend.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            // default status code
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                success = false,
                message = ex.Message,
                // hanya tampilkan detail kalau development
                detail = _env.IsDevelopment() ? ex.StackTrace : null
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
