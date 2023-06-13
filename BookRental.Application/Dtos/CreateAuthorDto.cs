using System.ComponentModel.DataAnnotations;

namespace BookRental.Application.Dtos;

#nullable disable

public class CreateAuthorDto
{
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
	public DateTime Born { get; set; }
}
