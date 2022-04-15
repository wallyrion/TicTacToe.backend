using System.Net;
using Newtonsoft.Json;
using TicTacToe.BLL.Exceptions;

namespace TicTacToe.API.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    public ErrorHandlerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var customException = error as AppException;
            bool isWriteToLogger = customException?.IsWriteToLogger == true;

            if (isWriteToLogger)
            {
                _logger.LogError(error, "Unhandled exception");
            }

            if (error is ValidationException)
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

            var result = JsonConvert.SerializeObject(new { message = error.Message });
            await response.WriteAsync(result);
        }
    }
}