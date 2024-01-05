using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

		builder.HasData(new ApplicationUser
		{
			Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
			Email = "test@mail.ru",
			NormalizedEmail = "TEST@MAIL.RU",
			FirstName = "TestFirst",
			LastName = "TestLast",
			UserName = "admin123",
			NormalizedUserName = "ADMIN123",
			PasswordHash = hasher.HashPassword(null, "admin123"),
			EmailConfirmed = true
		});
	}
}
