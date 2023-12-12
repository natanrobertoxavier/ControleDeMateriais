using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class BorrowedRepository : IBorrowedMaterialReadOnly, IBorrowedMaterialWriteOnly
{
    public async Task Add(List<BorrowedMaterial> borrowedMaterial)
    {
        var collection = ConnectDataBase.GetBorrowedMaterialAccess();
        await collection.InsertManyAsync(borrowedMaterial);
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
