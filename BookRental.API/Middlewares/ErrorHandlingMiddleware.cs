using BookRental.Application.Exceptions;
using System.Net;

namespace BookRental.API.Middlewares;

public class ErrorHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ErrorHandlingMiddleware> _logger;

	public ErrorHandlingMiddleware(
		RequestDelegate next,
		ILogger<ErrorHandlingMiddleware> logger)
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
		catch (Exception error)
		{
			_logger.LogError(error, error.Message);

			await HandleExceptionAsync(context, error);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception error)
	{
		var code = HttpStatusCode.InternalServerError;

		if (error is NotFoundException)
			code = HttpStatusCode.NotFound;

		if (error is InvalidPublicationYearException || error is BadRequestException)
			code = HttpStatusCode.BadRequest;

		context.Response.StatusCode = (int)code;

		return context.Response.WriteAsJsonAsync(error.Message);
	}
}
