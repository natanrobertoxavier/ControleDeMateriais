using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;

namespace ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
public interface IBorrowedMaterialReadOnly
{
    Task<List<BorrowedMaterial>> RecoverAll();
    Task<List<BorrowedMaterial>> RecoverForHashId(string hashId);
    Task<List<string>> RecoverBorrowedMaterial(List<string> codeBar);
}
