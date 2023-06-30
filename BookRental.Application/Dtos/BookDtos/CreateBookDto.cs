using System.ComponentModel.DataAnnotations;

namespace BookRental.Application.Dtos.BookDtos;

#nullable disable

public class CreateBookDto
{
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public string AuthorName { get; set; }
    public string Genre { get; set; }
}
