
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;

namespace ControleDeMateriais.Application.UseCases.BorrowedMaterials.Recover;
public class RecoverBorrowedMaterialsUseCase : IRecoverBorrowedMaterialsUseCase
{
    private readonly IBorrowedMaterialReadOnly _repositoryBorrowedMaterialReadOnly;

    public RecoverBorrowedMaterialsUseCase(
        IBorrowedMaterialReadOnly repositoryBorrowedMaterialReadOnly)
    {
        _repositoryBorrowedMaterialReadOnly = repositoryBorrowedMaterialReadOnly;
    }

    public async Task<List<string>> Execute(List<string> codeBar)
    {
        return await _repositoryBorrowedMaterialReadOnly.RecoverBorrowedMaterial(codeBar);
    }
}
