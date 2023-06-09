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
			new Genre() { Name = "Fantasy" },
			new Genre() { Name = "ScienceFiction" },
			new Genre() { Name = "Mystery" },
			new Genre() { Name = "Horror" },
			new Genre() { Name = "Thriller" },
			new Genre() { Name = "Romance" },
			new Genre() { Name = "Biography" },
			new Genre() { Name = "Guide" },
			new Genre() { Name = "History" },
			new Genre() { Name = "Travel" },
			new Genre() { Name = "Other" });

		builder.HasMany(b => b.Books)
			.WithOne(g => g.Genre)
			.HasForeignKey(b => b.GenreId);
	}
}
