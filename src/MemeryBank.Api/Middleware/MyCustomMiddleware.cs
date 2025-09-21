namespace MemeryBank.Api.Middleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Middleware -Starts");
            await next(context);
            await context.Response.WriteAsync("Custom Middleware - Continues");
        }
    }

    public static class MyCustomMiddlewareExt
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
