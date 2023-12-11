using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.Register;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class MaterialsForCollaboratorRepository : IMaterialsForCollaboratorWriteOnly
{
    public async Task Register(MaterialsForCollaborator materialsForCollaborator)
    {
        var collection = ConnectDataBase.GetMaterialsForCollaboratorAccess();
        await collection.InsertOneAsync(materialsForCollaborator);
    }
}
