namespace ControleDeMateriais.Domain.Repositories.User.ForgotPassword.Forgot;
public interface IForgotPasswordSendMailOnlyRepository
{
    Task SendMailRecoveryCode(Entities.User user, string recoveryCode);
}
