using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

#nullable disable

public sealed class Genre : BaseEntity
{
	public string Name { get; set; }
	public List<Book> Books { get; set; } = new();
}
