using MediatR;

namespace Application.Features.Category.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<List<CategoryResponse>>;
