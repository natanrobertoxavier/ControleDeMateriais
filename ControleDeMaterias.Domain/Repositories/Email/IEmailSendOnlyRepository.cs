namespace ControleDeMateriais.Domain.Repositories.Email;
public interface IEmailSendOnlyRepository
{
    Task SendMail(string content, string subject, string recipient);
}
