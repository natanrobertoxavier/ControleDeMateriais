
using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Devolution;
public class MaterialDevolutionUseCase : IMaterialDevolutionUseCase
{
    private readonly IBorrowedMaterialWriteOnly _repositoryMaterialWriteOnly;
    private readonly ILoggedUser _loggedUser;

    public MaterialDevolutionUseCase(
        IBorrowedMaterialWriteOnly repositoryMaterialWriteOnly,
        ILoggedUser loggedUser)
    {
        _repositoryMaterialWriteOnly = repositoryMaterialWriteOnly;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestMaterialDevolutionJson request)
    {
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
}
