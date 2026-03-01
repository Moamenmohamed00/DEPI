
namespace ApiSessions.Middleware
{
    public class TestMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {
            
            Console.WriteLine(">>>Test Middleware: Before next middleware");
            await _next(context);
            Console.WriteLine("<<<Test Middleware: After next middleware");
        }
    }
    public static class TestMiddlewareExtensions
    {
        public static IApplicationBuilder UseTestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TestMiddleware>();
        }
    }
}
