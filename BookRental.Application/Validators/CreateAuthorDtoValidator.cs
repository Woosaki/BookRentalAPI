using BookRental.Application.Dtos.AuthorDtos;
using BookRental.Infrastructure;
using FluentValidation;

namespace BookRental.Application.Validators;

public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
			.MaximumLength(50).WithMessage("Name's length cannot exceed 50 characters.");

        RuleFor(x => x.Born)
            .NotEmpty().WithMessage("Born date is required.");
	}
}
