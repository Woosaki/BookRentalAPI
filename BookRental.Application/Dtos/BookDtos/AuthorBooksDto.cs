namespace BookRental.Application.Dtos.BookDtos;

#nullable disable

public class AuthorBooksDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public string Genre { get; set; }
}