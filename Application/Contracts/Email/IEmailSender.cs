using Application.Models.Email;

namespace Application.Contracts.Email;

public interface IEmailSender
{
	Task SendEmailAsync(EmailMessage email);
}
