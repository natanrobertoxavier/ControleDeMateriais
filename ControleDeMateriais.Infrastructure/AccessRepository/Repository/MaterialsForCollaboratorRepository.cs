using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.Register;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class MaterialsForCollaboratorRepository : IMaterialsForCollaboratorWriteOnly
{
    public async Task Add(MaterialsForCollaborator materialsForCollaborator)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        await collection.InsertOneAsync(materialsForCollaborator);
    }

    public async Task Confirm(string hashId)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        var filter = Builders<MaterialsForCollaborator>.Filter.Where(c => c.MaterialsHashId.Equals(hashId));

        var updateObject = Builders<MaterialsForCollaborator>.Update
            .Set(c => c.Confirmed, true)
            .Set(c => c.DateTimeConfirmation, DateTime.UtcNow);

        await collection.UpdateOneAsync(filter, updateObject);
    }
}
