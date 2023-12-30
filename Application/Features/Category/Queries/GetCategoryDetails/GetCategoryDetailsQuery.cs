using MediatR;

namespace Application.Features.Category.Queries.GetAllCategoriesDetails;

public record GetCategoryDetailsQuery(int Id) : IRequest<CategoryDetailsResponse>;
