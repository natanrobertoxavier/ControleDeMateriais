using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class BorrowedRepository : IBorrowedMaterialWriteOnly, IBorrowedMaterialReadOnly
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
            .Set(c => c.UserReceivedId, materialDevolution.UserReceived)
            .Set(c => c.DateReceived, materialDevolution.DateReceived);

        await collection.UpdateManyAsync(filter, updateObject);
    }

    public async Task<List<BorrowedMaterial>> RecoverAll()
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        var filter = Builders<BorrowedMaterial>.Filter.Empty;

        return await collection.Find(filter).ToListAsync();
    }

    public async Task<List<BorrowedMaterial>> RecoverByStatus(bool status)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        var filter =  Builders<BorrowedMaterial>.Filter.Where(c => c.Active == status);

        return await collection.Find(filter).ToListAsync();
    }

    public async Task<List<BorrowedMaterial>> RecoverByStatusReceived(bool status, bool received)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        
        FilterDefinition<BorrowedMaterial> filter;
        var objectId = new MongoDB.Bson.ObjectId("000000000000000000000000");

        if (received)
        {
            filter = Builders<BorrowedMaterial>.Filter.Where(c => c.Active == status &&
                                                            c.UserReceivedId != objectId);
        }
        else
        {
            filter = Builders<BorrowedMaterial>.Filter.Where(c => c.Active == status &&
                                                            c.UserReceivedId == objectId);
        }

        return await collection.Find(filter).ToListAsync();
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

    public async Task<List<BorrowedMaterial>> RecoverByBarCode(string barCode)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        var filter = Builders<BorrowedMaterial>.Filter.Where(c => c.BarCode.Equals(barCode));

        return await collection.Find(filter).ToListAsync();
    }

    public async Task<List<BorrowedMaterial>> RecoverByHashId(string hashId)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        var filter = Builders<BorrowedMaterial>.Filter.Where(c => c.HashId.Equals(hashId));

        return await collection.Find(filter).ToListAsync();
    }

    public async Task<List<BorrowedMaterial>> RecoverByHashIdAndStatus(string hashId, bool status)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        var filter = Builders<BorrowedMaterial>.Filter.Where(c => c.HashId.Equals(hashId) &
                                                            c.Active.Equals(status));

        return await collection.Find(filter).ToListAsync();
    }
}
