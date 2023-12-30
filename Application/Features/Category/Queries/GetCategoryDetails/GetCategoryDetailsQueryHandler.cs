using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Queries.GetAllCategoriesDetails;

public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, CategoryDetailsResponse>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;

	public GetCategoryDetailsQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}

	public async Task<CategoryDetailsResponse> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
	{
		// Получить данные из базы данных
		Domain.Category category = await _categoryRepository.GetByIdAsync(request.Id);

		// Проверить, что объект существует
		if (category == null)
			throw new NotFoundException(nameof(Domain.Category), request.Id);

		// Преобразовать элемент к CategoryDetailsResponse
		CategoryDetailsResponse categoryDetailsResponse = _mapper.Map<CategoryDetailsResponse>(category);

		// Вернуть список
		return categoryDetailsResponse;
	}
}
