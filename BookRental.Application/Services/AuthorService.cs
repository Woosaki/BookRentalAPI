using AutoMapper;
using BookRental.Application.Dtos;
using BookRental.Application.Interfaces;
using BookRental.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Application.Services;

public class AuthorService : IAuthorService
{
	private readonly BookRentalDbContext _context;
	private readonly IMapper _mapper;

	public AuthorService(BookRentalDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<List<AuthorDto>> GetAuthorsAsync()
	{
		var authors = await _context.Authors
				.Take(10)
				.Include(a => a.Books)
				.ThenInclude(b => b.Genre)
				.ToListAsync();

		var authorsDtos = _mapper.Map<List<AuthorDto>>(authors);

		return authorsDtos;
	}

	public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
	{
		var author = await _context.Authors
			.Include(a => a.Books)
			.ThenInclude(b => b.Genre)
			.FirstOrDefaultAsync(a => a.Id == id);

		var authorDto = _mapper.Map<AuthorDto>(author);

		return authorDto;
	}
}
