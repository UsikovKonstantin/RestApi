using Application.Contracts.Persistence;
using Application.Features.Category.Shared;
using FluentValidation;

namespace Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

		Include(new BaseCategoryCommandValidator());

		RuleFor(c => c)
            .MustAsync(CategoryNameUnique).WithMessage("Category with this name already exists");
	}

	private async Task<bool> CategoryNameUnique(CreateCategoryCommand command, CancellationToken token)
	{
		Domain.Category? category = await _categoryRepository.GetCategoryByNameAsync(command.Name);
        return category == null;
	}
}
