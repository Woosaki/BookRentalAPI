using Bogus;
using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure;

public static class BookRentalDbContextSeed
{
	public static async Task SeedAsync(BookRentalDbContext? context)
	{
		if (context == null)
			throw new ArgumentNullException(nameof(context));

		if (!context.Authors.Any() && context.Database.IsRelational())
		{
			var authorFaker = new Faker<Author>()
				.RuleFor(a => a.Name, f => f.Name.FullName())
				.RuleFor(a => a.Born, f => f.Date.Past(70, DateTime.Now.AddYears(-20)));

			var authors = authorFaker.Generate(1000);

			await context.Authors.AddRangeAsync(authors);

			var genres = await context.Genres.ToListAsync();

			foreach (var author in authors)
			{
				var booksForCurrentAuthor = new Faker<Book>()
					.RuleFor(b => b.Title, f => f.Lorem.Sentence())
					.RuleFor(b => b.PublicationYear, f => f.Date.Between(author.Born, DateTime.Now).Year)
					.RuleFor(b => b.Author, author)
					.RuleFor(b => b.Genre, f => f.PickRandom(genres))
					.GenerateBetween(1, 7);

				await context.Books.AddRangeAsync(booksForCurrentAuthor);
			}
		}

		await context.SaveChangesAsync();
	}
}
