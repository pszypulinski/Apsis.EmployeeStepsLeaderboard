using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Apsis.EmployeeStepsLeaderboard.Api.ExceptionHandlers
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ValidationException validationException)
            {
                return false;
            }

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = exception.GetType().Name,
                Title = "Bad Request",
                Detail = JsonSerializer.Serialize(validationException.ValidationErrors),
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            }, cancellationToken);

            return true;
        }
    }
}
