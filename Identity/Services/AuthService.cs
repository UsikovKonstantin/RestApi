using Application.Contracts.Identity;
using Application.Exceptions;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services;

public class AuthService : IAuthService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager,
		IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
		_signInManager = signInManager;
		_jwtSettings = jwtSettings.Value;
    }

    public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
	{
		// Создаем пользователя
		ApplicationUser user = new ApplicationUser
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			UserName = request.UserName,
			Email = request.Email,
			EmailConfirmed = false
		};

		// Добавляем в базу данных
		IdentityResult result = await _userManager.CreateAsync(user, request.Password);

		if (result.Succeeded)
		{
			// Добавляем роль "User"
			await _userManager.AddToRoleAsync(user, "User");
			return new RegistrationResponse
			{
				UserId = user.Id,
			};
		}
		else
		{
			StringBuilder sb = new StringBuilder();
			foreach (IdentityError error in result.Errors)
			{
				sb.AppendFormat("{0}\n", error.Description);
			}
			throw new BadRequestException(sb.ToString());
		}
	}

	public async Task<ConfirmRegistrationResponse> ConfirmRegistrationAsync(ConfirmRegistrationRequest request)
	{
		throw new NotImplementedException();
	}

	public async Task<AuthResponse> LoginAsync(AuthRequest request)
	{
		ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

		if (user == null)
		{
			throw new NotFoundException(nameof(ApplicationUser), request.Email);
		}

		if (!user.EmailConfirmed)
		{
			throw new BadRequestException($"Email '{request.Email}' wasn't confirmed");
		}

		SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

		if (!result.Succeeded)
		{
			throw new BadRequestException($"Credentials for '{request.Email}' aren't valid");
		}

		JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

		AuthResponse response = new AuthResponse
		{
			Id = user.Id,
			Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
			Email = user.Email ?? "",
			UserName = user.UserName ?? ""
		};

		return response;
	}

	private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
	{
		IEnumerable<Claim> userClaims = await _userManager.GetClaimsAsync(user);
		IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

		IEnumerable<Claim> roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

		IEnumerable<Claim> claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
			new Claim("uid", user.Id)
		}
		.Union(userClaims)
		.Union(roleClaims);

		SymmetricSecurityKey symmKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

		SigningCredentials signinCredentials = new SigningCredentials(symmKey, SecurityAlgorithms.HmacSha256);

		JwtSecurityToken token = new JwtSecurityToken(
			issuer: _jwtSettings.Issuer, 
			audience: _jwtSettings.Audience, 
			claims: claims, 
			expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes), 
			signingCredentials: signinCredentials);

		return token;
	}
}
