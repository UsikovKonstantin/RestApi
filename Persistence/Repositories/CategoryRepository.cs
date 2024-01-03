using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ProductDatabaseContext context) : base(context)
    {
        
    }

	public async Task<Category?> GetCategoryByNameAsync(string name)
	{
		return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
	}
}
