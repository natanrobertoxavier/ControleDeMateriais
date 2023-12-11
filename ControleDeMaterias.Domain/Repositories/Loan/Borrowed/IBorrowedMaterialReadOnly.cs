namespace ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
public interface IBorrowedMaterialReadOnly
{
    Task<List<string>> RecoverBorrowedMaterial(List<string> codeBar);
}
