using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
	public void Configure(EntityTypeBuilder<Genre> builder)
	{
		builder.Property(g => g.Name)
			.IsRequired();

		builder.HasData(
			new Genre() { Id = 1, Name = "Fantasy" },
			new Genre() { Id = 2, Name = "ScienceFiction" },
			new Genre() { Id = 3, Name = "Mystery" },
			new Genre() { Id = 4, Name = "Horror" },
			new Genre() { Id = 5, Name = "Thriller" },
			new Genre() { Id = 6, Name = "Romance" },
			new Genre() { Id = 7, Name = "Biography" },
			new Genre() { Id = 8, Name = "Guide" },
			new Genre() { Id = 9, Name = "History" },
			new Genre() { Id = 10, Name = "Travel" },
			new Genre() { Id = 11, Name = "Other" });

		builder.HasMany(b => b.Books)
			.WithOne(g => g.Genre)
			.HasForeignKey(b => b.GenreId);
	}
}
