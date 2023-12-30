using MediatR;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Unit>
{
	public string Name { get; set; } = string.Empty;

	public string? Description { get; set; }
}
