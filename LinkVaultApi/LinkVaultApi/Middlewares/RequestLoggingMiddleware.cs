using System.Diagnostics;

namespace LinkVaultApi.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var stopwatch=Stopwatch.StartNew();
            _logger.LogInformation(">>request {method},{path},{query}",context.Request.Method,context.Request.Path,context.Request.Query);
            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation("<<response {method},{path},{statuscode}", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);

        }
    }
}
