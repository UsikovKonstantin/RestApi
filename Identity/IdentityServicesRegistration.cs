﻿using Application.Contracts.Identity;
using Application.Models.Identity;
using Identity.Context;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity;

public static class IdentityServicesRegistration
{
	public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

		services.AddDbContext<IdentityDbContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("ProductIdentityConnectionString"));
		});

		services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<IdentityDbContext>()
			.AddDefaultTokenProviders();

		services.AddTransient<IAuthService, AuthService>();
		services.AddTransient<IUserService, UserService>();

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(opts =>
		{
			opts.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateIssuerSigningKey = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero,
				ValidIssuer = configuration["JwtSettings:Issuer"],
				ValidAudience = configuration["JwtSettings:Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
			};
		});

		return services;
	}
}
