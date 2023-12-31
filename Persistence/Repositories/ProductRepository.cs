using Application.Contracts.Persistence;
using Domain;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
	public ProductRepository(ProductDatabaseContext context) : base(context)
	{

	}
}