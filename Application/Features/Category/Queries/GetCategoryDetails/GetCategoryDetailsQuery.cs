using MediatR;

namespace Application.Features.Category.Queries.GetAllCategoriesDetails;

public class GetCategoryDetailsQuery: IRequest<CategoryDetailsResponse>
{
    public int Id { get; set; }
}
