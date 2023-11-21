namespace ControleDeMateriais.Domain.Repositories.User.ForgotPassword.Forgot;
public interface IForgotPasswordSendMailOnlyRepository
{
    Task SendMail(Entities.User user, string recoveryCode);
}
