using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

#nullable disable

public sealed class BookBorrow : BaseEntity
{
	public int BookId { get; set; }
	public int UserId { get; set; }
	public Book Book { get; set; }
	public User User { get; set; }
	public DateTime BorrowDate { get; set; }
	public DateTime? ReturnDate { get; set; }
}
