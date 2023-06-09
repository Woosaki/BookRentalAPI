using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.Property(b => b.Title)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(b => b.PublicationYear)
			.IsRequired();

		builder.HasOne(b => b.Author)
			.WithMany(a => a.Books)
			.HasForeignKey(b => b.AuthorId);

		builder.HasOne(b => b.Genre)
			.WithMany(g => g.Books)
			.HasForeignKey(b => b.GenreId);
	}
}
