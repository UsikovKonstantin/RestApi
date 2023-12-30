using MediatR;

namespace Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<int>
{
	public string Name { get; set; } = string.Empty;

	public string? Description { get; set; }
}
