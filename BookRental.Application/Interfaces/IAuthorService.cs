using BookRental.Application.Dtos.AuthorDtos;

namespace BookRental.Application.Interfaces;

public interface IAuthorService
{
	Task<List<AuthorDto>> GetAsync();
	Task<AuthorDto> GetByIdAsync(int id);
	Task<AuthorDto> CreateAsync(CreateAuthorDto dto);
	Task DeleteAsync(int id);
}
