using System.ComponentModel.DataAnnotations;

namespace BookRental.Application.Dtos.AuthorDtos;

#nullable disable

public class CreateAuthorDto
{
    public string Name { get; set; }
    public DateTime Born { get; set; }
}
