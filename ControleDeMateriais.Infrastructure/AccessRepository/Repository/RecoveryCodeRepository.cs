using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.User.ForgotPassword.RecoveryCode;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class RecoveryCodeRepository : IRecoveryCodeWriteOnlyRepository, IRecoveryCodeReadOnlyRepository
{
    public async Task Add(RecoveryCode code)
    {
        var collection = ConnectDataBase.GetRecoveryCodeAccess();
        await collection.InsertOneAsync(code);
    }

    public async Task InactivateCode(RecoveryCode recoveryCode)
    {
        var collection = ConnectDataBase.GetRecoveryCodeAccess();
        var filter = Builders<RecoveryCode>.Filter.Where(o => o.Id == recoveryCode.Id);
        var codeToInactivate = await collection.Find(filter).FirstOrDefaultAsync();

        codeToInactivate.Active = false;

        collection.ReplaceOne(filter, codeToInactivate);
    }

    public async Task<RecoveryCode> IsThereCodeActive(User user)
    {
        var collection = ConnectDataBase.GetRecoveryCodeAccess();
        var filter = Builders<RecoveryCode>.Filter.Where(o => o.UserId == user.Id && o.Active == true);

        var result = await collection.Find(filter).FirstOrDefaultAsync();

        if (result is not null)
            return result;

        return null;
    }
}
