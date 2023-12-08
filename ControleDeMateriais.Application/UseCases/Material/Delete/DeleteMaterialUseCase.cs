using AutoMapper;
using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Application.UseCases.Material.Recover;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation.Results;

namespace ControleDeMateriais.Application.UseCases.Material.Delete;
public class DeleteMaterialUseCase : IDeleteMaterialUseCase
{
    private readonly IRecoverMaterialUseCase _recoverMaterialUseCase;
    private readonly IMaterialWriteOnlyRepository _repositoryMaterialWriteOnly;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public DeleteMaterialUseCase(
        IRecoverMaterialUseCase recoverMaterialUseCase, 
        IMaterialWriteOnlyRepository repositoryMaterialWriteOnly, 
        ILoggedUser loggedUser, 
        IMapper mapper)
    {
        _recoverMaterialUseCase = recoverMaterialUseCase;
        _repositoryMaterialWriteOnly = repositoryMaterialWriteOnly;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task Execute(string barCode)
    {
        var material = await ValidateData(barCode);
        var user = await _loggedUser.RecoveryUser();

        var materialLog = _mapper.Map<Domain.Entities.MaterialDeletionLog>(material);
        materialLog.UserIdDeleted = user.Id;
        await _repositoryMaterialWriteOnly.RegisterDeletionLog(materialLog);

        await _repositoryMaterialWriteOnly.Delete(barCode);
    }

    private async Task<Domain.Entities.Material> ValidateData(string barCode)
    {
        var validator = new RecoverMaterialValidator();
        var resultValidation = validator.Validate(barCode);
        var material = await _recoverMaterialUseCase.Execute(barCode);

        if (material is null)
        {
            resultValidation.Errors.Add(new ValidationFailure("BarCode", ErrorMessagesResource.NENHUM_MATERIAL_LOCALIZADO));
        }

        if (!resultValidation.IsValid)
        {
            var messageError = resultValidation.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
            throw new ExceptionValidationErrors(messageError);
        }

        var result = _mapper.Map<Domain.Entities.Material>(material);

        return result;
    }
}
