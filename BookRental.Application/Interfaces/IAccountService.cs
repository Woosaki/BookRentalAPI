using BookRental.Application.Dtos;

namespace BookRental.Application.Interfaces;

public interface IAccountService
{
	Task RegisterAsync(RegisterUserDto dto);
}
