using BookRental.Application.Dtos.UserDtos;
using FluentValidation;

namespace BookRental.Application.Validators;

public class ChangeRoleDtoValidator : AbstractValidator<ChangeRoleDto>
{
	public ChangeRoleDtoValidator()
	{
		RuleFor(x => x.UserId)
			.NotEmpty().WithMessage("User Id is required.");

		RuleFor(x => x.RoleId)
			.NotEmpty().WithMessage("Role Id is required.")
			.Must(x => x == 1 || x == 2 || x == 3).WithMessage("Role Id must be either 1 (User), 2 (Manager), or 3 (Admin).");
	}
}
