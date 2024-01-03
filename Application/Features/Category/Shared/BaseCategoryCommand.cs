namespace Application.Features.Category.Shared;

public class BaseCategoryCommand
{
	public string Name { get; set; } = string.Empty;

	public string? Description { get; set; }
}
