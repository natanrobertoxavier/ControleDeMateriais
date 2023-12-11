
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Recover;
public class RecoverBorrowedMaterialUseCase : IRecoverBorrowedMaterialUseCase
{
    private readonly IBorrowedMaterialReadOnly _repositoryBorrowedMaterialReadOnly;

    public RecoverBorrowedMaterialUseCase(
        IBorrowedMaterialReadOnly repositoryBorrowedMaterialReadOnly)
    {
        _repositoryBorrowedMaterialReadOnly = repositoryBorrowedMaterialReadOnly;
    }

    public async Task<List<string>> Execute(List<string> codeBar)
    {
        return await _repositoryBorrowedMaterialReadOnly.RecoverBorrowedMaterial(codeBar);
    }
}
