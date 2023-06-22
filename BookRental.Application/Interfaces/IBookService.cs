using BookRental.Application.Dtos.BookDtos;

namespace BookRental.Application.Interfaces;

public interface IBookService
{
	Task<List<BookDto>> GetAsync();
	Task<BookDto> GetByIdAsync(int id);
	Task<BookDto> CreateAsync(CreateBookDto dto);
	Task DeleteAsync(int id);
}
