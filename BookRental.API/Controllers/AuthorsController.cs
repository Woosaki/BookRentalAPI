using BookRental.Application.Dtos;
using BookRental.Application.Interfaces;
using BookRental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.API.Controllers
{
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
		public async Task<ActionResult<List<AuthorDto>>> GetAuthorsAsync()
		{
			var authors = await _authorService.GetAuthorsAsync();

			if (!authors.Any())
				return NotFound();

			return Ok(authors);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<AuthorDto>> GetAuthorAsync([FromRoute] int id)
		{
			var author = await _authorService.GetAuthorByIdAsync(id);

			if (author is null)
				return NotFound();

			return Ok(author);
		}
	}
}
