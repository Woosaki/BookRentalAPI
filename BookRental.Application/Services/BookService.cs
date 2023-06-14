using BookRental.Application.Dtos;
using BookRental.Application.Exceptions;
using BookRental.Application.Interfaces;
using BookRental.Domain.Entities;
using BookRental.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace BookRental.Application.Services;

public class BookService : IBookService
{
	private readonly BookRentalDbContext _context;
	private readonly IMapper _mapper;
	private readonly ILogger<BookService> _logger;

	public BookService(
		BookRentalDbContext context,
		IMapper mapper,
		ILogger<BookService> logger)
	{
		_context = context;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<List<BookDto>> GetAsync()
	{
		_logger.LogInformation("GET method invoked for all books");

		var books = await _context.Books
			.Take(10)
			.Include(b => b.Author)
			.Include(b => b.Genre)
			.ToListAsync();

		var booksDto = _mapper.Map<List<BookDto>>(books);

		return booksDto;
	}

	public async Task<BookDto> GetByIdAsync(int id)
	{
		_logger.LogInformation($"GET method invoked for a book with ID {id}");

		var book = await _context.Books
			.Include(b => b.Author)
			.Include(b => b.Genre)
			.FirstOrDefaultAsync(b => b.Id == id)
			?? throw new NotFoundException($"Book with ID {id} not found");

		var bookDto = _mapper.Map<BookDto>(book);

		return bookDto;
	}

	public async Task<BookDto> CreateAsync(CreateBookDto dto)
	{
		_logger.LogInformation("POST method invoked for a book");

		var author = await _context.Authors
			.FirstOrDefaultAsync(a => EF.Functions.Like(a.Name, dto.AuthorName))
			?? throw new NotFoundException($"Author with name {dto.AuthorName} not found");

		var genre = await _context.Genres
			.FirstOrDefaultAsync(g => EF.Functions.Like(g.Name, dto.Genre))
			?? throw new NotFoundException($"Genre named {dto.Genre} not found");

		if (dto.PublicationYear < author.Born.Year)
		{
			throw new InvalidPublicationYearException(
				$"Book publication year {dto.PublicationYear}" +
				$" is before the author's birth year of {author.Born.Year}");
		}

		var book = new Book
		{
			Title = dto.Title,
			PublicationYear = dto.PublicationYear,
			Author = author,
			Genre = genre
		};

		await _context.Books.AddAsync(book);
		await _context.SaveChangesAsync();

		var bookDto = _mapper.Map<BookDto>(book);

		_logger.LogInformation($"Book created with ID {bookDto.Id}");

		return bookDto;
	}

	public async Task DeleteAsync(int id)
	{
		_logger.LogInformation($"DELETE method invoked for a book with ID {id}");

		var book = await _context.Books
			.FirstOrDefaultAsync(a => a.Id == id)
			?? throw new NotFoundException($"Book with ID {id} not found");

		_context.Books.Remove(book);
		await _context.SaveChangesAsync();

		_logger.LogInformation($"Book deleted with ID {book.Id}");
	}
}
