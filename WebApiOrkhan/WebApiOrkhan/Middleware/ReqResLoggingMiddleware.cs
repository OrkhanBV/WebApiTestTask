using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

/*Links about Logging Meddleware--->
 https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/
 https://metanit.com/sharp/aspnet5/2.10.php
 https://blog.elmah.io/asp-net-core-request-logging-middleware/
 */


namespace WebApiOrkhan.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        public RequestResponseLoggingMiddleware(RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                .CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {requestStream}" );
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}");
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}

/*
        Классу потребуется конструктор, который принимает два аргумента, 
        оба будут предоставлены системой внедрения зависимостей ASP.NET Core. 
        Первый - это RequestDelegate, который станет следующим meddleware 
        в конвейере. 
        
        Второй - это экземпляр ILoggerFactory, 
        который будет использоваться для создания регистратора. RequestDelegate 
        хранится на уровне класса  _next переменной и loggerFactory используется 
        для создания регистратора , который хранится на уровне класса _logger переменной.
        
        The class will need a constructor that takes two arguments both will be 
        provided by ASP.NET Core’s dependency injection system. The first is a RequestDelegate 
        which will be the next piece of middleware in the pipeline. 
        The second is an instance of an ILoggerFactory which will 
        be used to create a logger. The RequestDelegate is stored to the class 
        level _next variable and the loggerFactory is used to create a logger 
        that is stored to the class level _logger variable.
        
*/
       
/*
        Добавьте функцию Invoke, которая будет вызываться, когда ваш middleware 
        запускается конвейером. Ниже приводится функция, которая не делает ничего, 
        кроме вызова следующего middleware в конвейере. 
        
        Add an Invoke function which is the function that will be called when your 
        middleware is run by the pipeline. The following is the function that does 
        nothing other than call the next middleware in the pipeline.
*/
        
/*
         Затем добавьте статический класс, чтобы упростить добавление промежуточного 
         программного обеспечения (meddleware) в конвейер приложения. Это тот же самый шаблон, который 
         использует встроенное промежуточное ПО (middleware)
         
         Next, add a static class to simplify adding the middleware to the
         application’s pipeline. This is the same pattern the built-in middleware uses.
         
         Like this ---> RequestResponseLoggingMiddlewareExtensions
*/

/*
 * Вопросы
 * 1) что по хорошему мы должны логгировать??
 * 2) на сколько нам важно обработать requestStream в удобный нам вид??
 * 3) что можно было бы улучшить?
 */