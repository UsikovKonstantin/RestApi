using Application.Contracts.Identity;
using Application.Exceptions;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services;

public class UserService : IUserService
{
	private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
	{
		IEnumerable<ApplicationUser> applicationUsers = await _userManager.GetUsersInRoleAsync("User");
		return applicationUsers.Select(u => new UserResponse
		{
			Id = u.Id,
			Email = u.Email ?? "",
			FirstName = u.FirstName,
			LastName = u.LastName,
		}).ToList();
	}

	public async Task<UserResponse> GetUserByIdAsync(string userId)
	{
		ApplicationUser? applicationUser = await _userManager.FindByIdAsync(userId);

		if (applicationUser == null)
		{
			throw new NotFoundException(nameof(ApplicationUser), userId);
		}

		return new UserResponse
		{
			Id = applicationUser.Id,
			Email = applicationUser.Email ?? "",
			FirstName = applicationUser.FirstName,
			LastName = applicationUser.LastName,
		};
	}
}
