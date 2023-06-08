using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

#nullable disable

public sealed class Book : BaseEntity
{
	public string Title { get; set; }
	public int PublicationYear { get; set; }
	public Author Author { get; set; }
	public Genre Genre { get; set; }	
	public int AuthorId { get; set; }
	public int GenreId { get; set; }
}
