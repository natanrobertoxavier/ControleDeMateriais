using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class MaterialsForCollaboratorRepository : IMaterialsForCollaboratorWriteOnly, IMaterialForCollaboratorReadOnly
{
    public async Task Add(MaterialsForCollaborator materialsForCollaborator)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        await collection.InsertOneAsync(materialsForCollaborator);
    }

    public async Task Confirm(string hashId, string userId)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        var filter = Builders<MaterialsForCollaborator>.Filter.Where(c => c.MaterialsHashId.Equals(hashId));

        var updateObject = Builders<MaterialsForCollaborator>.Update
            .Set(c => c.UserIdConfirmed, new ObjectId(userId))
            .Set(c => c.Confirmed, true)
            .Set(c => c.DateTimeConfirmation, DateTime.UtcNow);

        await collection.UpdateOneAsync(filter, updateObject);
    }

    public async Task<MaterialsForCollaborator> RecoverByHashId(string hashId)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        var filter = Builders<MaterialsForCollaborator>.Filter.Where(c => c.MaterialsHashId.Equals(hashId));
        var result = await collection.Find(filter).FirstOrDefaultAsync() ?? null;

        return result;
    }
}
