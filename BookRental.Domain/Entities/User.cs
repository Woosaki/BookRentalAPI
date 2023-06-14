using BookRental.Domain.Common;
using System.Security.Principal;

namespace BookRental.Domain.Entities;

#nullable disable

public sealed class User : BaseEntity
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string PasswordHash { get; set; }
	public Role Role { get; set; }
	public int RoleId { get; set; }
}
