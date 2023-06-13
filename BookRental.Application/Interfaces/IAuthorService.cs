using BookRental.Application.Dtos;
using BookRental.Domain.Entities;

namespace BookRental.Application.Interfaces;

public interface IAuthorService
{
	Task<List<AuthorDto>> GetAsync();
	Task<AuthorDto?> GetByIdAsync(int id);
	Task<Author> CreateAsync(CreateAuthorDto dto);
	Task DeleteAsync(int id);
}
