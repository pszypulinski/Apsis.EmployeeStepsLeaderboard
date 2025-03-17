using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Apsis.EmployeeStepsLeaderboard.Api.ExceptionHandlers
{
    public class ConcurrencyExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ConcurrencyException concurrencyException)
            {
                return false;
            }

            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Type = exception.GetType().Name,
                Title = "Conflict",
                Detail = concurrencyException.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            }, cancellationToken);

            return true;
        }
    }
}
