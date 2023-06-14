using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.HasData(
			new Role { Id = 1, Name = "User" },
			new Role { Id = 2, Name = "Manager" },
			new Role { Id = 3, Name = "Admin" });
	}
}
