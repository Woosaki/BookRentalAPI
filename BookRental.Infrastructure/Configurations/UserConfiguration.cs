﻿using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.Property(u => u.Name)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(u => u.Email)
			.IsRequired()
			.HasMaxLength(255);

		builder.Property(u => u.PasswordHash)
			.IsRequired();

		builder.HasIndex(u => u.Email)
			.IsUnique();
	}
}
