using BookRental.Application.Dtos;
using BookRental.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.API.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorsController : ControllerBase
{
	private readonly IAuthorService _authorService;

	public AuthorsController(IAuthorService authorService)
	{
		_authorService = authorService;
	}

	[HttpGet]
	public async Task<ActionResult<List<AuthorDto>>> GetAllAsync()
	{
		var authors = await _authorService.GetAsync();

		if (!authors.Any())
			return NotFound();

		return Ok(authors);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<AuthorDto>> GetAsync([FromRoute] int id)
	{
		var author = await _authorService.GetByIdAsync(id);

		if (author is null)
		{
			return NotFound();
		}

		return Ok(author);
	}

	[HttpPost]
	public async Task<ActionResult> CreateAsync([FromBody] CreateAuthorDto dto)
	{
		var author = await _authorService.CreateAsync(dto);

		return Created($"/api/authors/{author.Id}", null);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAsync([FromRoute] int id)
	{
		await _authorService.DeleteAsync(id);

		return NoContent();
	}
}
