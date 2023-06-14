using BookRental.Application.Dtos;
using BookRental.Domain.Entities;

namespace BookRental.Application.Interfaces;

public interface IBookService
{
	Task<List<BookDto>> GetAsync();
	Task<BookDto> GetByIdAsync(int id);
	Task<BookDto> CreateAsync(CreateBookDto dto);
	Task DeleteAsync(int id);
}
