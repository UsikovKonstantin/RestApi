using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseContext;

public class ProductDatabaseContext : DbContext
{
    public ProductDatabaseContext(DbContextOptions<ProductDatabaseContext> options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDatabaseContext).Assembly);

		base.OnModelCreating(modelBuilder);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		DateTime now = DateTime.Now;
		foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
			.Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
		{
			entry.Entity.ModifiedDate = now;

			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedDate = now;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}
}
