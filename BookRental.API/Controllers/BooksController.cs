using BookRental.Application.Dtos;
using BookRental.Application.Interfaces;
using BookRental.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.API.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
	private readonly IBookService _bookService;

	public BooksController(IBookService bookService)
	{
		_bookService = bookService;
	}

	[HttpGet]
	public async Task<ActionResult<List<BookDto>>> GetAllAsync()
	{
		var books = await _bookService.GetAsync();

		return Ok(books);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<BookDto>> GetAsync([FromRoute] int id)
	{
		var book = await _bookService.GetByIdAsync(id);

		return Ok(book);
	}

	[HttpPost]
	public async Task<ActionResult<BookDto>> CreateAsync([FromBody] CreateBookDto dto)
	{
		var bookDto = await _bookService.CreateAsync(dto);

		return Created($"/api/books/{bookDto.Id}", bookDto);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync([FromRoute] int id)
	{
		await _bookService.DeleteAsync(id);

		return NoContent();
	}
}
