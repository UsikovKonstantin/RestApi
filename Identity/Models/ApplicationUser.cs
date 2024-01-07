using Microsoft.AspNetCore.Identity;

namespace Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
	public string? Token { get; set; }
	public DateTime? TokenExpiredDate { get; set; }
}
