using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using TicTacToe.BLL.Exceptions;

namespace TicTacToe.API.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<ErrorHandlerMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
        
        catch (Exception error)
        {
            var elapsed = sw.ElapsedMilliseconds;
            var response = context.Response;
            response.ContentType = "application/json";

            var customException = error as AppException;
            bool isWriteToLogger = customException?.IsWriteToLogger ?? true;

            if (isWriteToLogger)
            {
                _logger.LogError(error, $"Unhandled exception. ElapsedMilliseconds: {elapsed}");
            }

            if (error is InvalidRequestException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (error is EntityNotFoundException)
            {
                response.StatusCode = (int) HttpStatusCode.NotFound;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var result = JsonConvert.SerializeObject(new
            {
                elapsed,
                message = error.Message,
                trace = error.StackTrace
            });
            await response.WriteAsync(result);
        }
    }
}