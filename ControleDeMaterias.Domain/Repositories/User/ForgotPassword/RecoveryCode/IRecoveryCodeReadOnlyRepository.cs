using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Repositories.User.ForgotPassword.RecoveryCode;
public interface IRecoveryCodeReadOnlyRepository
{
    Task<Entities.RecoveryCode> IsThereCodeActive(Entities.User user);
}
