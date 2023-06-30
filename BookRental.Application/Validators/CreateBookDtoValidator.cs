using Bogus.DataSets;
using BookRental.Application.Dtos.BookDtos;
using BookRental.Infrastructure;
using FluentValidation;

namespace BookRental.Application.Validators;

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
	public CreateBookDtoValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty().WithMessage("Title is required.")
			.MaximumLength(100).WithMessage("Book's title length cannot exceed 100 characters.");

		RuleFor(x => x.PublicationYear)
			.NotEmpty().WithMessage("Publication year is required.")
			.LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("Publication year cannot be from the future.");

		RuleFor(x => x.AuthorName)
			.NotEmpty().WithMessage("Author's name is required.")
			.MaximumLength(50).WithMessage("Author's name length cannot exceed 50 characters.");

		RuleFor(x => x.Genre)
			.NotEmpty().WithMessage("Genre is required.");
	}
}
