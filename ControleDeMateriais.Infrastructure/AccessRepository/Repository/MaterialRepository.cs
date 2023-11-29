using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class MaterialRepository : IMaterialWriteOnlyRepository, IMaterialReadOnlyRepository
{
    public async Task Register(Material material)
    {
        var collection = ConnectDataBase.GetMaterialAccess();
        await collection.InsertOneAsync(material);
    }

    public async Task<Material> Update(string id, Material material)
    {
        var collection = ConnectDataBase.GetMaterialAccess();
        var filter = Builders<Material>.Filter.Where(c => c.BarCode.Equals(material.BarCode));

        var updateObject = Builders<Material>.Update
            .Set(c => c.Name, material.Name)
            .Set(c => c.Description, material.Description)
            .Set(c => c.Category, material.Category);

        await collection.UpdateOneAsync(filter, updateObject);

        var result = await RecoverById(id);

        return result;
    }

    public async Task<List<Material>> RecoverAll()
    {
        var collection = ConnectDataBase.GetMaterialAccess();
        var filter = Builders<Material>.Filter.Empty;

        return await collection.Find(filter).ToListAsync();
    }

    public async Task<List<Material>> RecoverByCategory(int category)
    {
        var collection = ConnectDataBase.GetMaterialAccess();
        var filter = Builders<Material>.Filter.Where(c => c.Category.Equals(category));
        var result = await collection.Find(filter).ToListAsync();

        return result;
    }

    public async Task<Material> RecoverById(string id)
    {
        var collection = ConnectDataBase.GetMaterialAccess();

        ObjectId objectId;
        
        ObjectId.TryParse(id, out objectId);

        var filter = Builders<Material>.Filter.Where(c => c.Id.Equals(objectId));

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
