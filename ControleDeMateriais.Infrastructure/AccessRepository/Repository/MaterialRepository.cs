using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Material;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class MaterialRepository : IMaterialWriteOnlyRepository
{
    public async Task Register(Material material)
    {
        var collection = ConnectDataBase.GetMaterialAccess();
        await collection.InsertOneAsync(material);
    }
}
