using Application.Models.Identity;

namespace Application.Contracts.Identity;

public interface IAuthService
{
	Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);

	Task<ConfirmRegistrationResponse> ConfirmRegistrationAsync(ConfirmRegistrationRequest request);

	Task<AuthResponse> LoginAsync(AuthRequest request);
}
