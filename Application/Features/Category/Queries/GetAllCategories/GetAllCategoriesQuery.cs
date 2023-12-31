using MediatR;

namespace Application.Features.Category.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResponse>>
{
}
