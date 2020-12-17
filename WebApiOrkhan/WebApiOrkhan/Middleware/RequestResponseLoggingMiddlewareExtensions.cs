using Microsoft.AspNetCore.Builder;

/*
 Links about Logging Meddleware--->
 https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/
 https://metanit.com/sharp/aspnet5/2.10.php
 https://blog.elmah.io/asp-net-core-request-logging-middleware/
*/

namespace WebApiOrkhan.Middleware
{
    
    /*
    Затем добавьте статический класс, чтобы упростить добавление промежуточного 
    программного обеспечения в конвейер приложения. Это тот же самый шаблон, который 
    использует встроенное промежуточное ПО (middleware)
         
    Next, add a static class to simplify adding the middleware to the
    application’s pipeline. This is the same pattern the built-in middleware uses.
         
    Like under
    */
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
    
    /*
    Adding to the pipeline 
    
    To add the new middleware to the pipeline open the Startup.cs
    file and add the following line to the Configure function.
    app.UseRequestResponseLogging();
    */
}