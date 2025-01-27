using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProblemDetailHandleError.ExHandlers
{
    public class CustomExceptionHandler1 : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            int status = exception switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = status;

            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = "An Error Occur",
                Type = exception.GetType().Name,
                Detail = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }


    public class NewCustomHandlerException(IProblemDetailsService problemDetailService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails()
            {
                Status = exception switch
                {
                    ArgumentException=> StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                },
                Title = "An error occurred",
                Type = exception.GetType().Name,
                Detail = exception.Message
            };


            return await problemDetailService.TryWriteAsync(new ProblemDetailsContext
            {
                Exception = exception,
                HttpContext = httpContext,
                ProblemDetails = problemDetails
            });
        }
    }
}
