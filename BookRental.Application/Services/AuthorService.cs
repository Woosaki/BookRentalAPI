using AutoMapper;
using BookRental.Application.Dtos.AuthorDtos;
using BookRental.Application.Exceptions;
using BookRental.Application.Interfaces;
using BookRental.Domain.Entities;
using BookRental.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookRental.Application.Services;

public class AuthorService : IAuthorService
{
	private readonly BookRentalDbContext _context;
	private readonly IMapper _mapper;
	private readonly ILogger<AuthorService> _logger;

	public AuthorService(
		BookRentalDbContext context,
		IMapper mapper,
		ILogger<AuthorService> logger)
	{
		_context = context;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<List<AuthorDto>> GetAsync()
	{
		_logger.LogInformation("GET method invoked for all authors");

		var authors = await _context.Authors
				.Take(10)
				.Include(a => a.Books)
				.ThenInclude(b => b.Genre)
				.ToListAsync();

		var authorsDtos = _mapper.Map<List<AuthorDto>>(authors);

		return authorsDtos;
	}

	public async Task<AuthorDto> GetByIdAsync(int id)
	{
		_logger.LogInformation($"GET method invoked for an author with ID {id}");

		var author = await _context.Authors
			.Include(a => a.Books)
			.ThenInclude(b => b.Genre)
			.FirstOrDefaultAsync(a => a.Id == id)
			?? throw new NotFoundException($"Author with ID {id} not found");

		var authorDto = _mapper.Map<AuthorDto>(author);

		return authorDto;
	}

	public async Task<AuthorDto> CreateAsync(CreateAuthorDto dto)
	{
		_logger.LogInformation("POST method invoked for an author");

		var author = _mapper.Map<Author>(dto);

		await _context.Authors.AddAsync(author);
		await _context.SaveChangesAsync();

		var authorDto = _mapper.Map<AuthorDto>(author);

		_logger.LogInformation($"Author created with ID {author.Id}");

		return authorDto;
	}

	public async Task DeleteAsync(int id)
	{
		_logger.LogInformation($"DELETE method invoked for an author with ID {id}");

		var author = await _context.Authors
			.FirstOrDefaultAsync(a => a.Id == id)
			?? throw new NotFoundException($"Author with ID {id} not found");

		_context.Authors.Remove(author);
		await _context.SaveChangesAsync();

		_logger.LogInformation($"Author deleted with ID {author.Id}");
	}
}
