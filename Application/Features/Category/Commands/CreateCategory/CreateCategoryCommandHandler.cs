using Application.Contracts.Persistence;
using AutoMapper;
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
		// TODO: Проверить входящие данные


		// Преобразовать элемент к Category
		Domain.Category category = _mapper.Map<Domain.Category>(request);

		// Добавить объект в базу данных
		await _categoryRepository.CreateAsync(category);

		// Вернуть id
		return category.Id;
	}
}
