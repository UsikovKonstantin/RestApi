using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
	public ProductRepository(ProductDatabaseContext context) : base(context)
	{

	}


	public async Task<Product?> GetProductByNameAsync(string name)
	{
		return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
	}

	public async Task<IEnumerable<Product>> GetAllProductsWithDetailsAsync()
	{
		return await _context.Products.AsNoTracking()
			.Include(p => p.Category)
			.ToListAsync();
	}

	public async Task<Product?> GetProductByIdWithDetailsAsync(int id)
	{
		return await _context.Products.AsNoTracking()
			.Include(p => p.Category)
			.FirstOrDefaultAsync(p => p.Id == id);
	}

	public async Task<IEnumerable<Product>> GetProductsByCategoryIdWithDetailsAsync(int id)
	{
		return await _context.Products.AsNoTracking()
			.Where(p => p.CategoryId == id)
			.Include(p => p.Category)
			.ToListAsync();
	}
}