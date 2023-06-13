using BookRental.API.Middlewares;

namespace BookRental.API.Extensions;

public static class AppExtensions
{
	public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder) =>
		builder.UseMiddleware<ErrorHandlingMiddleware>();
}
