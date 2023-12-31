using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		DateTime now = DateTime.Now;
		builder.HasData(
			new Category
			{
				Id = 1,
				Name = "name1",
				Description = "description1",
				CreatedDate = now,
				ModifiedDate = now,
			},
			new Category
			{
				Id = 2,
				Name = "name2",
				Description = "description2",
				CreatedDate = now,
				ModifiedDate = now,
			}
		);

		builder.HasKey(c => c.Id);
		builder.Property(c => c.Id).ValueGeneratedOnAdd();

		builder.Property(c => c.Name)
			.IsRequired()
			.HasMaxLength(50);
		builder.HasIndex(c => c.Name)
			.IsUnique();

		builder.Property(c => c.Description)
			.HasMaxLength(100);

		builder.Property(c => c.CreatedDate)
			.IsRequired();

		builder.Property(c => c.ModifiedDate)
			.IsRequired();
	}
}
