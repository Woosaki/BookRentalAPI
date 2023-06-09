using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure.Configurations;

public class BookBorrowConfiguration : IEntityTypeConfiguration<BookBorrow>
{
	public void Configure(EntityTypeBuilder<BookBorrow> builder)
	{
		builder.Property(b => b.BorrowDate)
			.HasDefaultValueSql("getutcdate()");

		builder.HasOne(bb => bb.User)
			.WithMany()
			.HasForeignKey(bb => bb.UserId);

		builder.HasOne(bb => bb.Book)
			.WithMany()
			.HasForeignKey(bb => bb.BookId);
	}
}
