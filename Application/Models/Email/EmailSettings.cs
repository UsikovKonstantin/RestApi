namespace Application.Models.Email;

public class EmailSettings
{
	public string FromMail { get; set; } = string.Empty;
	public string FromPassword { get; set; } = string.Empty;
	public string Smtp { get; set; } = string.Empty;
	public int Port { get; set; } = 0;
}
