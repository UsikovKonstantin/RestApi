using Api.Models;
using Application.Exceptions;
using System.Net;

namespace Api.Middleware;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
		try
		{
			await _next(httpContext);
		}
		catch (Exception exception)
		{
			await HandleExceptionAsync(httpContext, exception);
		}
    }

	private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
	{
		HttpStatusCode statusCode;
		CustomProblemDetails problemDetails;

		switch (exception)
		{
			case BadRequestException badRequestException:
				statusCode = HttpStatusCode.BadRequest;
				problemDetails = new CustomProblemDetails
				{
					Title = badRequestException.Message,
					Status = (int)statusCode,
					Detail = badRequestException.InnerException?.Message,
					Type = nameof(BadRequestException),
					Errors = badRequestException.ValidationErrors
				};
				break;
			case NotFoundException notFoundException:
				statusCode = HttpStatusCode.NotFound;
				problemDetails = new CustomProblemDetails
				{
					Title = notFoundException.Message,
					Status = (int)statusCode,
					Detail = notFoundException.InnerException?.Message,
					Type = nameof(NotFoundException),
				};
				break;
			default:
				statusCode = HttpStatusCode.InternalServerError;
				problemDetails = new CustomProblemDetails
				{
					Title = exception.Message,
					Status = (int)statusCode,
					Detail = exception.StackTrace,
					Type = nameof(HttpStatusCode.InternalServerError),
				};
				break;
		}

		httpContext.Response.StatusCode = (int)statusCode;
		await httpContext.Response.WriteAsJsonAsync(problemDetails);
	}
}
