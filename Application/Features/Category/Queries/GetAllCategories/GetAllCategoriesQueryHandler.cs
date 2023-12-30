using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
	{
        // Получить данные из базы данных
        List<Domain.Models.Category> categories = await _categoryRepository.GetAllAsync();

		// Преобразовать элементы к CategoryResponse
        List<CategoryResponse> categoryResponses = _mapper.Map<List<CategoryResponse>>(categories);

		// Вернуть список
        return categoryResponses;
	}
}
