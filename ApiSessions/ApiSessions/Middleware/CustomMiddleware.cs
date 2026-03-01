namespace ApiSessions.Middleware
{
    public class CustomMiddleware
    {
        //middleware is inline-custom middleware(class or inherit from imiddleware)
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine(">>>Custom Middleware: Before next middleware");
            await _next(context);
            Console.WriteLine("<<<Custom Middleware: After next middleware");
        }
    }
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
