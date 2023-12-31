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

	public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		// Проверить входящие данные
		UpdateCategoryCommandValidator validator = new UpdateCategoryCommandValidator(_categoryRepository);
		ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid)
			throw new BadRequestException("Invalid Category", validationResult);

		// Преобразовать элемент к Category
		Domain.Category category = _mapper.Map<Domain.Category>(request);

		// Обновить объект в базе данных
		await _categoryRepository.UpdateAsync(category);

		// Вернуть Unit
		return Unit.Value;
	}
}
