using Application.Features.Category.Shared;
using MediatR;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommand : BaseCategoryCommand, IRequest<Unit>
{
    public int Id { get; set; }
}
