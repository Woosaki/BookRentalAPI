namespace BookRental.Application.Dtos.UserDtos;

#nullable disable

public class RegisterUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
