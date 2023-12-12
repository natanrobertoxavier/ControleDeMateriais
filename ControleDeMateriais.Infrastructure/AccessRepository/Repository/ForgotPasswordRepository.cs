using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Email;
using ControleDeMateriais.Domain.Repositories.User.ForgotPassword.Forgot;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class ForgotPasswordRepository : IForgotPasswordSendMailOnlyRepository
{
    private readonly IEmailSendOnlyRepository _emailSendOnlyRepository;

    public ForgotPasswordRepository(
        IEmailSendOnlyRepository emailSendOnlyRepository)
    {
        _emailSendOnlyRepository = emailSendOnlyRepository;
    }

    public async Task SendMailRecoveryCode(User user, string recoveryCode)
    {
        var content = InsertContentRecoveryCode(user, recoveryCode);
        var subject = "Código para recuperação de senha";
        var recipient = user.Email;

        await _emailSendOnlyRepository.SendMail(content, subject, recipient);
    }

    private static string InsertContentRecoveryCode(User user, string recoveryCode)
    {
        return string.Concat($"Olá {user.Name}, aqui está seu código para recuperação de senha: {recoveryCode}. Esse código é valido por 30 minutos.");
    }
}
