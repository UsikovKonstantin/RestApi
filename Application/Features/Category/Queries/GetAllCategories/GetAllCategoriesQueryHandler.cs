using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
	{
        // Получить данные из базы данных
        IEnumerable<Domain.Category> categories = await _categoryRepository.GetAllAsync();

		// Преобразовать элементы к CategoryResponse
		IEnumerable<CategoryResponse> categoryResponses = _mapper.Map<IEnumerable<CategoryResponse>>(categories);

		// Вернуть список
        return categoryResponses;
	}
}
