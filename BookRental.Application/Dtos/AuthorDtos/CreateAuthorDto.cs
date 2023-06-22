using System.ComponentModel.DataAnnotations;

namespace BookRental.Application.Dtos.AuthorDtos;

#nullable disable

public class CreateAuthorDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    public DateTime Born { get; set; }
}
