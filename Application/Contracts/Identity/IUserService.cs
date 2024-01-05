using Application.Models.Identity;

namespace Application.Contracts.Identity;

public interface IUserService
{
	Task<IEnumerable<UserResponse>> GetAllUsersAsync();

	Task<UserResponse> GetUserByIdAsync(string userId);
}
