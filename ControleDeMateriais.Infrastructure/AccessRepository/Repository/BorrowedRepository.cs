using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class BorrowedRepository : IBorrowedMaterialReadOnly, IBorrowedMaterialWriteOnly
{
    public async Task Add(List<BorrowedMaterial> borrowedMaterial)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        await collection.InsertManyAsync(borrowedMaterial);
    }

    public async Task Confirm(string hashId)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        var filter = Builders<BorrowedMaterial>.Filter.Where(c => c.HashId.Equals(hashId));

        var updateObject = Builders<BorrowedMaterial>.Update
            .Set(c => c.Active, true);

        await collection.UpdateManyAsync(filter, updateObject);
    }

    public async Task Devolution(MaterialDevolution materialDevolution)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        var filter = Builders<BorrowedMaterial>.Filter.And(
            Builders<BorrowedMaterial>.Filter.Eq(c => c.HashId, materialDevolution.HashId),
            Builders<BorrowedMaterial>.Filter.In(c => c.BarCode, materialDevolution.BarCode)
        );


        var updateObject = Builders<BorrowedMaterial>.Update
            .Set(c => c.Active, false)
            .Set(c => c.UserReceived, materialDevolution.UserReceived)
            .Set(c => c.DateReceived, materialDevolution.DateReceived);

        await collection.UpdateManyAsync(filter, updateObject);
    }

    public async Task<List<string>> RecoverBorrowedMaterial(List<string> codeBar)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        var filter = Builders<BorrowedMaterial>.Filter.In(x => x.BarCode, codeBar) &
                     Builders<BorrowedMaterial>.Filter.Eq(x => x.Active, true);

        var resultQuery = await collection.Find(filter).ToListAsync();

        var result = resultQuery?.Select(item => item.BarCode).ToList();

        if (result.Any())
            return result;

        return null;
    }
}
