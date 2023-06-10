using BookRental.Application.Dtos;

namespace BookRental.Application.Interfaces;

public interface IAuthorService
{
	Task<List<AuthorDto>> GetAuthorsAsync();
	Task<AuthorDto?> GetAuthorByIdAsync(int id);
}
