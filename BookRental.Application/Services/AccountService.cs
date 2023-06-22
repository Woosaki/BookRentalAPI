using BookRental.Application.Dtos.UserDtos;
using BookRental.Application.Interfaces;
using BookRental.Domain.Entities;
using BookRental.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace BookRental.Application.Services;

public class AccountService : IAccountService
{
	private readonly BookRentalDbContext _context;
	private readonly IPasswordHasher<User> _passwordHasher;

	public AccountService(
		BookRentalDbContext context,
		IPasswordHasher<User> passwordHasher)
	{
		_context = context;
		_passwordHasher = passwordHasher;
	}

	public async Task RegisterAsync(RegisterUserDto dto)
	{
		var newUser = new User
		{
			Name = dto.Name,
			Email = dto.Email,
			RoleId = dto.RoleId
		};

		newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);

		await _context.Users.AddAsync(newUser);
		await _context.SaveChangesAsync();
	}
}
