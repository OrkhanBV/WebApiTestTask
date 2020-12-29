using Microsoft.AspNetCore.Builder;

namespace BabaevTask5.Middleware
{
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}

/*
 Links about Logging Meddleware--->
 https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/
 https://metanit.com/sharp/aspnet5/2.10.php
 https://blog.elmah.io/asp-net-core-request-logging-middleware/
*/