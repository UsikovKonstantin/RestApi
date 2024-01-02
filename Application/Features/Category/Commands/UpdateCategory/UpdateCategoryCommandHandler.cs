using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using FluentValidation.Results;
using MediatR;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;
	private readonly IAppLogger<UpdateCategoryCommandHandler> _logger;

	public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IAppLogger<UpdateCategoryCommandHandler> logger)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		// Проверить входящие данные
		UpdateCategoryCommandValidator validator = new UpdateCategoryCommandValidator(_categoryRepository);
		ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid)
		{
			_logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(Category), request.Id);
			throw new BadRequestException("Invalid Category", validationResult);
		}

		// Преобразовать элемент к Category
		Domain.Category category = _mapper.Map<Domain.Category>(request);

		// Назначить CreatedDate.
		Domain.Category? categoryInDatabase = await _categoryRepository.GetByIdAsync(request.Id);
		category.CreatedDate = categoryInDatabase?.CreatedDate;

		// Обновить объект в базе данных
		await _categoryRepository.UpdateAsync(category);
		_logger.LogInformation("Category updated successfully");

		// Вернуть Unit
		return Unit.Value;
	}
}
