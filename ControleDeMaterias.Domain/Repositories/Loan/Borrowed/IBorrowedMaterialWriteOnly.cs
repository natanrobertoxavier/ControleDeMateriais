using ControleDeMateriais.Domain.Entities;
using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
public interface IBorrowedMaterialWriteOnly
{
    Task Add(List<BorrowedMaterial> borrowedMaterial);
    Task Confirm(string hashId);
    Task Devolution(MaterialDevolution materialDevolution);
    Task Delete(string hashId);
    Task RegisterDeletionLog(List<BorrowedMaterialDeletionLog> borrowedMaterialsLog);
}
