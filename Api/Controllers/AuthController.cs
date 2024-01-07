using Application.Contracts.Identity;
using Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		public readonly IAuthService _authServise;

        public AuthController(IAuthService authServise)
        {
            _authServise = authServise;
        }

		[HttpPost("register")]
		public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
		{
			return Ok(await _authServise.RegisterAsync(request));
		}

		[HttpPost("login")]
		public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
		{
			return Ok(await _authServise.LoginAsync(request));
		}

		[HttpPost("confirmRegistration")]
		public async Task<IActionResult> ConfirmRegistration(ConfirmRegistrationRequest request)
		{
			await _authServise.ConfirmRegistrationAsync(request);
			return Ok();
		}
	}
}
