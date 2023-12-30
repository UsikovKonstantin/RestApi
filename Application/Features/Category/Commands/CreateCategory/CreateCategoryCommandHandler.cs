using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using FluentValidation.Results;
using MediatR;

namespace Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;

	public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}

	public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		// Проверить входящие данные
		CreateCategoryCommandValidator validator = new CreateCategoryCommandValidator(_categoryRepository);
		ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid)
			throw new BadRequestException("Invalid Category", validationResult);

		// Преобразовать элемент к Category
		Domain.Category category = _mapper.Map<Domain.Category>(request);

		// Добавить объект в базу данных
		await _categoryRepository.CreateAsync(category);

		// Вернуть id
		return category.Id;
	}
}
