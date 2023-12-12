using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Domain.Repositories.Loan.Register;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Confirm;
public class ConfirmSelectedMaterialUseCase : IConfirmSelectedMaterialUseCase
{
    private readonly IMaterialsForCollaboratorWriteOnly _repositoryMaterialsForCollaboratorWriteOnly;

    public ConfirmSelectedMaterialUseCase(
        IMaterialsForCollaboratorWriteOnly repositoryMaterialsForCollaboratorWriteOnly)
    {
        _repositoryMaterialsForCollaboratorWriteOnly = repositoryMaterialsForCollaboratorWriteOnly;
    }

    public async Task Execute(RequestConfirmSelectedMaterialJson request)
    {
        await _repositoryMaterialsForCollaboratorWriteOnly.Confirm(request.HashId);
    }
}
