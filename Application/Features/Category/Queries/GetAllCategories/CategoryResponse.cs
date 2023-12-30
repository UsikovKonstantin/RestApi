namespace Application.Features.Category.Queries.GetAllCategories;

public class CategoryResponse
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string? Description { get; set; }

	public DateTime? CreatedDate { get; set; }

	public DateTime? ModifiedDate { get; set; }
}
