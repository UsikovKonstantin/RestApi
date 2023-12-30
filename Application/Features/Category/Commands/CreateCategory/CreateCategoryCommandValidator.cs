using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50 characters");

		RuleFor(c => c.Description)
		   .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characters");

        RuleFor(c => c)
            .MustAsync(CategoryNameUnique)
            .WithMessage("Category already exists");
	}

	private async Task<bool> CategoryNameUnique(CreateCategoryCommand command, CancellationToken token)
	{
		Domain.Category? category = await _categoryRepository.GetCategoryByNameAsync(command.Name);
        return category == null;
	}
}
