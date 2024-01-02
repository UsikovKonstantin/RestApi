using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		DateTime now = DateTime.Now;
		builder.HasData(
			new Product
			{
				Id = 1,
				Name = "name1",
				Description = "description1",
				Price = 10000,
				Weight = 1000,
				ExpirationDate = now.AddYears(1),
				CreatedDate = now,
				ModifiedDate = now,
				CategoryId = 1
			},
			new Product
			{
				Id = 2,
				Name = "name2",
				Description = "description2",
				Price = 20000,
				Weight = 2000,
				ExpirationDate = now.AddYears(2),
				CreatedDate = now,
				ModifiedDate = now,
				CategoryId = 2
			}
		);

		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id).ValueGeneratedOnAdd();

		builder.Property(p => p.Name)
			.IsRequired()
			.HasMaxLength(50);
		builder.HasIndex(p => p.Name)
			.IsUnique();

		builder.Property(p => p.Description)
			.HasMaxLength(100);

		builder.Property(p => p.Price)
			.HasColumnType("decimal(8,2)")
			.IsRequired();

		builder.Property(p => p.Weight)
			.IsRequired();

		builder.Property(p => p.CreatedDate)
			.IsRequired();

		builder.Property(p => p.ModifiedDate)
			.IsRequired();

		builder.HasOne(p => p.Category)
			.WithMany(c => c.Products)
			.HasForeignKey(p => p.CategoryId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
