﻿using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
	private readonly ICategoryRepository _categoryRepository;

	public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;

		RuleFor(c => c.Id)
			.NotNull().WithMessage("{PropertyName} is required")
			.MustAsync(CategoryExists).WithMessage("Category doesn't exist");

		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("{PropertyName} is required")
			.MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50 characters");

		RuleFor(c => c.Description)
		   .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characters");

		RuleFor(c => c)
			.MustAsync(CategoryNameUnique).WithMessage("Category with this name already exists");
	}

	private async Task<bool> CategoryExists(int id, CancellationToken token)
	{
		Domain.Category? category = await _categoryRepository.GetByIdAsync(id);
		return category != null;
	}

	private async Task<bool> CategoryNameUnique(UpdateCategoryCommand command, CancellationToken token)
	{
		Domain.Category? category = await _categoryRepository.GetCategoryByNameAsync(command.Name);
		return category == null || category.Id == command.Id;
	}
}
