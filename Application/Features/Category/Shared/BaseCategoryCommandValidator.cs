using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Category.Shared;

public class BaseCategoryCommandValidator : AbstractValidator<BaseCategoryCommand>
{
	public BaseCategoryCommandValidator()
	{
		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("{PropertyName} is required")
			.MaximumLength(50).WithMessage("{PropertyName} must be fewer than {MaxLength} characters");

		RuleFor(c => c.Description)
		   .MaximumLength(100).WithMessage("{PropertyName} must be fewer than {MaxLength} characters");
	}
}
