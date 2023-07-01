using BookRental.Application.Dtos.UserDtos;

namespace BookRental.Application.Interfaces;

public interface IAccountService
{
	Task RegisterAsync(RegisterUserDto dto);
	Task<string> GenerateJwt(LoginUserDto dto);
	Task<List<UserDto>> GetAllAsync();
	Task<UserDto> ChangeRole(ChangeRoleDto dto);
}
