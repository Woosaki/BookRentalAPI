using AutoMapper;
using BookRental.Application.Dtos.UserDtos;
using BookRental.Application.Exceptions;
using BookRental.Application.Interfaces;
using BookRental.Domain.Entities;
using BookRental.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookRental.Application.Services;

public class AccountService : IAccountService
{
	private readonly BookRentalDbContext _context;
	private readonly IPasswordHasher<User> _passwordHasher;
	private readonly IConfiguration _configuration;
	private readonly IMapper _mapper;

	public AccountService(
		BookRentalDbContext context,
		IPasswordHasher<User> passwordHasher,
		IConfiguration configuration,
		IMapper mapper)
	{
		_context = context;
		_passwordHasher = passwordHasher;
		_configuration = configuration;
		_mapper = mapper;
	}

	public async Task RegisterAsync(RegisterUserDto dto)
	{
		var newUser = new User
		{
			Name = dto.Name,
			Email = dto.Email,
			RoleId = 1
		};

		newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);

		await _context.Users.AddAsync(newUser);
		await _context.SaveChangesAsync();
	}

	public async Task<string> GenerateJwt(LoginUserDto dto)
	{
		var user = await _context.Users
			.Include(u => u.Role)
			.FirstOrDefaultAsync(u => u.Email == dto.Email)
			?? throw new BadRequestException("Invalid username or password");

		var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
		if (verificationResult == PasswordVerificationResult.Failed)
		{
			throw new BadRequestException("Invalid username or password");
		}

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Name),
			new Claim(ClaimTypes.Role, user.Role.Name)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken
		(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Issuer"],
			claims: claims,
			expires: DateTime.UtcNow.AddDays(1),
			notBefore: DateTime.UtcNow,
			signingCredentials: cred
		);

		var tokenHandler = new JwtSecurityTokenHandler();
		return tokenHandler.WriteToken(token);
	}

	public async Task<List<UserDto>> GetAllAsync()
	{
		var accounts = await _context.Users
			.Include(u => u.Role)
			.ToListAsync();

		var accountDtos = _mapper.Map<List<UserDto>>(accounts);

		return accountDtos;
	}

	public async Task<UserDto> ChangeRole(ChangeRoleDto dto)
	{
		var user = await _context.Users
			.Include(u => u.Role)
			.FirstOrDefaultAsync(u => u.Id == dto.UserId)
			?? throw new BadRequestException("User with that Id doesn't exist.");

		user.RoleId = dto.RoleId;

		await _context.SaveChangesAsync();

		await _context.Entry(user).Reference(u => u.Role).LoadAsync();

		var userDto = _mapper.Map<UserDto>(user);

		return userDto;
	}
}
