using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
	private readonly IAppLogger<GetAllCategoriesQueryHandler> _logger;

	public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, IAppLogger<GetAllCategoriesQueryHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
	{
        // Получить данные из базы данных
        IEnumerable<Domain.Category> categories = await _categoryRepository.GetAllAsync();

		// Преобразовать элементы к CategoryResponse
		IEnumerable<CategoryResponse> categoryResponses = _mapper.Map<IEnumerable<CategoryResponse>>(categories);

        _logger.LogInformation("Get all categories was requested.");

		// Вернуть список
        return categoryResponses;
	}
}
