using Application.Contracts.Persistence;
using Application.Features.Category.Shared;
using FluentValidation;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
	private readonly ICategoryRepository _categoryRepository;

	public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;

		Include(new BaseCategoryCommandValidator());

		RuleFor(c => c)
			.MustAsync(CategoryNameUnique).WithMessage("Category with this name already exists");
	}

	private async Task<bool> CategoryNameUnique(UpdateCategoryCommand command, CancellationToken token)
	{
		Domain.Category? category = await _categoryRepository.GetCategoryByNameAsync(command.Name);
		return category == null || category.Id == command.Id;
	}
}
