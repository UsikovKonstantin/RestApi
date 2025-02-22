﻿using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
	{
		builder.HasData(new IdentityUserRole<string>
		{
			RoleId = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
			UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
		});
	}
}
