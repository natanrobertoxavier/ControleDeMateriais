using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Repositories.User.ForgotPassword.RecoveryCode;
public interface IRecoveryCodeWriteOnlyRepository
{
    Task Add(Entities.RecoveryCode code);
    Task InactivateCode(Entities.RecoveryCode recoveryCode);
}
