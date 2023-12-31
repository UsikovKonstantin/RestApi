using Domain;

namespace Application.Contracts.Persistence;

public interface IProductRepository : IGenericRepository<Product>
{
	Task<Product?> GetProductByNameAsync(string name);

	Task<IEnumerable<Product>> GetAllProductsWithDetailsAsync();

	Task<Product?> GetProductByIdWithDetailsAsync(int id);

	Task<IEnumerable<Product>> GetProductsByCategoryIdWithDetailsAsync(int id);
}
