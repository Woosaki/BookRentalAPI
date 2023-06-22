using BookRental.Application.Dtos.UserDtos;
using BookRental.Infrastructure;
using FluentValidation;

namespace BookRental.Application.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
	public RegisterUserDtoValidator(BookRentalDbContext dbContext)
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Name is required.")
			.MaximumLength(50).WithMessage("Name's length cannot exceed 50 characters.");

		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("Email is required.")
			.EmailAddress().WithMessage("Email is not valid.")
			.MaximumLength(255).WithMessage("Email's length cannot exceed 255 characters.")		
			.Custom((value, context) =>
			{
				var emailInUse = dbContext.Users.Any(u => u.Email == value);
				if (emailInUse)
				{
					context.AddFailure("Email", "Email already taken");
				}
			});

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("Password is required.")
			.Length(8, 20).WithMessage("Password must be between 8 and 20 characters long.");

		RuleFor(x => x.ConfirmPassword)
			.Equal(e => e.Password).WithMessage("Password's must match.");
	}
}
