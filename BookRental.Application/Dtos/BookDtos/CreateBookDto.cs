using System.ComponentModel.DataAnnotations;

namespace BookRental.Application.Dtos.BookDtos;

#nullable disable

public class CreateBookDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    [Range(1, 9999)]
    public int PublicationYear { get; set; }
    [Required]
    [MaxLength(50)]
    public string AuthorName { get; set; }
    [Required]
    public string Genre { get; set; }
}
