using BookRental.Domain.Entities;

namespace BookRental.Application.Dtos.UserDtos;

#nullable disable

public class UserDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Role { get; set; }
}
