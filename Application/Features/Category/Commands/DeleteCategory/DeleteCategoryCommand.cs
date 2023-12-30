using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
