using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure;

public class BookRentalDbContext : DbContext
{
	public DbSet<Author> Authors { get; set; }
	public DbSet<Book> Books { get; set; }
	public DbSet<BookBorrow> BookBorrows { get; set; }
	public DbSet<Genre> Genres { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Role> Roles { get; set; }

	public BookRentalDbContext(DbContextOptions<BookRentalDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(BookRentalDbContext).Assembly);
	}
}
