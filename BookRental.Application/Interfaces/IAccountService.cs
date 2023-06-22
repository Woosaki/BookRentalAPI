using BookRental.Application.Dtos.UserDtos;

namespace BookRental.Application.Interfaces;

public interface IAccountService
{
	Task RegisterAsync(RegisterUserDto dto);
}
