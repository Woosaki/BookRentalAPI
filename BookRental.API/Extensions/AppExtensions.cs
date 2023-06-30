using BookRental.API.Middlewares;
using BookRental.Infrastructure;

namespace BookRental.API.Extensions;

public static class AppExtensions
{
	public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app) =>
		app.UseMiddleware<ErrorHandlingMiddleware>();

	public static async Task SeedDatabaseAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var context = scope.ServiceProvider.GetService<BookRentalDbContext>();
		await BookRentalDbContextSeed.SeedAsync(context);
	}
}
