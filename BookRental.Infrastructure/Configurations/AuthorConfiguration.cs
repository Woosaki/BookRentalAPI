using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.Property(a => a.Name)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(a => a.Born)
			.HasColumnType("datetime2")
			.IsRequired();

		builder.HasMany(a => a.Books)
			.WithOne(a => a.Author)
			.HasForeignKey(b => b.AuthorId);
	}
}
