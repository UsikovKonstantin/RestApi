using Application.Contracts.Email;
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
using Application.Models.Email;

namespace Identity.Services;

public class AuthService : IAuthService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly JwtSettings _jwtSettings;
	private readonly IEmailSender _emailSender;

	public AuthService(UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager,
		IOptions<JwtSettings> jwtSettings,
		IEmailSender emailSender)
    {
        _userManager = userManager;
		_signInManager = signInManager;
		_jwtSettings = jwtSettings.Value;
		_emailSender = emailSender;
    }

    public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
	{
		ApplicationUser? foundUser = await _userManager.FindByEmailAsync(request.Email);
		if (foundUser != null && !foundUser.EmailConfirmed && foundUser.TokenExpiredDate < DateTime.UtcNow)
		{
			await _userManager.DeleteAsync(foundUser);
		}

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
		if (!result.Succeeded)
		{
			StringBuilder sb = new StringBuilder();
			foreach (IdentityError error in result.Errors)
			{
				sb.AppendFormat("{0}\n", error.Description);
			}
			throw new BadRequestException(sb.ToString());
		}

		// Генерируем токен
		JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
		user.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		user.TokenExpiredDate = jwtSecurityToken.ValidTo;

		result = await _userManager.UpdateAsync(user);
		if (!result.Succeeded)
		{
			StringBuilder sb = new StringBuilder();
			foreach (IdentityError error in result.Errors)
			{
				sb.AppendFormat("{0}\n", error.Description);
			}
			throw new BadRequestException(sb.ToString());	
		}

		// Отправляем сообщение
		await _emailSender.SendEmailAsync(new EmailMessage
		{
			ToMail = request.Email,
			Subject = "Регистрация",
			Message = "Ваш токен: " + user.Token
		});

		// Добавляем роль "User"
		await _userManager.AddToRoleAsync(user, "User");
		return new RegistrationResponse
		{
			UserId = user.Id,
		};
	}

	public async Task ConfirmRegistrationAsync(ConfirmRegistrationRequest request)
	{
		ApplicationUser? user = _userManager.Users.Where(u => u.Token == request.Token).FirstOrDefault();

		if (user == null)
		{
			throw new NotFoundException(nameof(ApplicationUser), request.Token);
		}

		if (user.TokenExpiredDate < DateTime.UtcNow)
		{
			throw new BadRequestException($"Token expired");
		}

		user.EmailConfirmed = true;
		user.Token = null;
		user.TokenExpiredDate = null;
		await _userManager.UpdateAsync(user);
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

		user.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		user.TokenExpiredDate = jwtSecurityToken.ValidTo;
		await _userManager.UpdateAsync(user);

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
			expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes), 
			signingCredentials: signinCredentials);

		return token;
	}
}
