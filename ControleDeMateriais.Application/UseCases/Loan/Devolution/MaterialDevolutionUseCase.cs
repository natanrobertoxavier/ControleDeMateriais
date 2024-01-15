using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Application.UseCases.Material.Recover;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.Loan.Devolution;
public class MaterialDevolutionUseCase : IMaterialDevolutionUseCase
{
    private readonly IBorrowedMaterialWriteOnly _repositoryMaterialWriteOnly;
    private readonly IMaterialsForCollaboratorReadOnly _repositoryMaterialForCollaboratorReadOnly;
    private readonly IRecoverMaterialUseCase _recoverMaterialUseCase;
    private readonly ILoggedUser _loggedUser;

    public MaterialDevolutionUseCase(
        IBorrowedMaterialWriteOnly repositoryMaterialWriteOnly,
        IMaterialsForCollaboratorReadOnly repositoryMaterialForCollaboratorReadOnly,
        IRecoverMaterialUseCase recoverMaterialUseCase,
        ILoggedUser loggedUser)
    {
        _repositoryMaterialWriteOnly = repositoryMaterialWriteOnly;
        _repositoryMaterialForCollaboratorReadOnly = repositoryMaterialForCollaboratorReadOnly;
        _recoverMaterialUseCase = recoverMaterialUseCase;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestMaterialDevolutionJson request)
    {
        await ValidateData(request);

        var user = await _loggedUser.RecoveryUser();

        var materialDevolution = new MaterialDevolution()
        {
            HashId = request.HashId,
            BarCode = request.BarCode,
            UserReceived = user.Id,
            DateReceived = DateTime.UtcNow,
        };

        await _repositoryMaterialWriteOnly.Devolution(materialDevolution);
    }

    private async Task ValidateData(RequestMaterialDevolutionJson request)
    {
        var loan = await _repositoryMaterialForCollaboratorReadOnly.RecoverByHashId(request.HashId) ??
            throw new ExceptionValidationErrors(new List<string> { ErrorMessagesResource.CONCESSAO_NAO_LOCALIZADA });

        if (!loan.Confirmed)
        {
            throw new ExceptionValidationErrors(new List<string> { ErrorMessagesResource.CONCESSAO_NAO_CONFIRMADA });
        }

        foreach (var barCode in request.BarCode)
        {
            _ = await _recoverMaterialUseCase.Execute(barCode) ??
            throw new ExceptionValidationErrors(new List<string> {
                string.Concat($"{ErrorMessagesResource.MATERIAL_NAO_LOCALIZADO_INICIAL} {barCode}") });
        }
    }
}
