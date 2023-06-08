using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

#nullable disable

public sealed class User : BaseEntity
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string PasswordHash { get; set; }
}
