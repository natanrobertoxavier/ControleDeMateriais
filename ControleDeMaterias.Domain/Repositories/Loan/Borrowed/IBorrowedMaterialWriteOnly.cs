using ControleDeMateriais.Domain.Entities;
using MongoDB.Bson;

namespace ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
public interface IBorrowedMaterialWriteOnly
{
    Task Add(List<BorrowedMaterial> borrowedMaterial);
}
