using LinkVaultApi.Exceptions;
using System.Net;

namespace LinkVaultApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate next,ILogger<GlobalExceptionMiddleware>logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var (StatusCode, Message) = ex switch
                {
                    Exceptions.Exceptions => (HttpStatusCode.NotFound, ex.Message),
                    BadRequestException => (HttpStatusCode.BadRequest, ex.Message),
                    DuplicateWaitObjectException => (HttpStatusCode.Conflict, ex.Message),
                    NotFoundException => (HttpStatusCode.NotFound, ex.Message),
                    _ => (HttpStatusCode.InternalServerError,"uncatched bug here"),
                };
                if (StatusCode == HttpStatusCode.InternalServerError)
                    _logger.LogError(ex.Message);
                else
                    _logger.LogWarning($"handeled{ex.GetType().Name}");

                context.Response.StatusCode =(int) StatusCode;
                context.Response.ContentType ="application/json";
                await context.Response.WriteAsJsonAsync(new { StatusCode,Message}/*context.Response*/);
            }
        }
    }
}
