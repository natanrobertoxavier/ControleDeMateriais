using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class MaterialsForCollaboratorRepository : IMaterialsForCollaboratorWriteOnly, IMaterialsForCollaboratorReadOnly
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
            .Set(c => c.CollaboratorConfirmedId, new ObjectId(userId))
            .Set(c => c.Confirmed, true)
            .Set(c => c.DateTimeConfirmation, DateTime.UtcNow);

        await collection.UpdateOneAsync(filter, updateObject);
    }

    public async Task<List<MaterialsForCollaborator>> RecoverAll()
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        var filter = Builders<MaterialsForCollaborator>.Filter.Empty;
        var result = await collection.Find(filter).ToListAsync() ?? null;

        return result;
    }

    public async Task<List<MaterialsForCollaborator>> RecoverByCollaborator(ObjectId id)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        var filter = Builders<MaterialsForCollaborator>.Filter.Where(c => c.CollaboratorId.Equals(id));
        var result = await collection.Find(filter).ToListAsync() ?? null;

        return result;
    }

    public async Task<List<MaterialsForCollaborator>> RecoverByCollaboratorLoanStatus(ObjectId id, bool status)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        var filter = Builders<MaterialsForCollaborator>.Filter.Where(c => c.CollaboratorId.Equals(id) &
                                                            c.Confirmed.Equals(status));
        var result = await collection.Find(filter).ToListAsync() ?? null;

        return result;
    }

    public async Task<MaterialsForCollaborator> RecoverByHashId(string hashId)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        var filter = Builders<MaterialsForCollaborator>.Filter.Where(c => c.MaterialsHashId.Equals(hashId));
        var result = await collection.Find(filter).FirstOrDefaultAsync() ?? null;

        return result;
    }

    public async Task<List<MaterialsForCollaborator>> RecoverByHashIds(List<string> hashId)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        var filter = Builders<MaterialsForCollaborator>.Filter.And(
            Builders<MaterialsForCollaborator>.Filter.In(c => c.MaterialsHashId, hashId));
        var result = await collection.Find(filter).ToListAsync() ?? null;

        return result;
    }

    public async Task<List<MaterialsForCollaborator>> RecoverByStatus(bool status)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        var filter = Builders<MaterialsForCollaborator>.Filter.Where(c => c.Confirmed.Equals(status));
        var result = await collection.Find(filter).ToListAsync() ?? null;

        return result;
    }
}
