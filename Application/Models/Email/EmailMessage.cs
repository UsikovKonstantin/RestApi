namespace Application.Models.Email;

public class EmailMessage
{
    public string ToMail { get; set; } = string.Empty;
	public string Subject { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
}
