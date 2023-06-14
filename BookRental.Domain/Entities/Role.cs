using BookRental.Domain.Common;

namespace BookRental.Domain.Entities;

#nullable disable

public class Role : BaseEntity
{
	public string Name { get; set; }
}
