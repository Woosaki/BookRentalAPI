using BookRental.Application.Dtos.BookDtos;

namespace BookRental.Application.Dtos.AuthorDtos;

#nullable disable

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Born { get; set; }
    public List<AuthorBooksDto> Books { get; set; }
}
