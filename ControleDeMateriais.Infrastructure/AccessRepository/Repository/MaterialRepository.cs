using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class MaterialRepository : IMaterialWriteOnlyRepository, IMaterialReadOnlyRepository
{
    public async Task Register(Material material)
    {
        var collection = ConnectDataBase.GetMaterialAccess();
        await collection.InsertOneAsync(material);
    }

    public async Task<Material> RecoverById(string Id)
    {
        var collection = ConnectDataBase.GetMaterialAccess();
        var filter = Builders<Material>.Filter.Where(c => c.BarCode.Equals(Id));

        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Material> RecoverByBarCode(string barCode)
    {
        var collection = ConnectDataBase.GetMaterialAccess();
        var filter = Builders<Material>.Filter.Where(c => c.BarCode.Equals(barCode));
        var result = await collection.Find(filter).FirstOrDefaultAsync() ?? null;

        return result;
    }
}
