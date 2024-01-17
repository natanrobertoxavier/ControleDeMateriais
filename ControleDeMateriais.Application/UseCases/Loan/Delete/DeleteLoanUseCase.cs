
using AutoMapper;
using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ControleDeMateriais.Application.UseCases.Loan.Delete;
public class DeleteLoanUseCase : IDeleteLoanUseCase
{
    private readonly IMapper _mapper;
    private readonly IMaterialsForCollaboratorWriteOnly _repositoryMaterialsForCollaboratorWriteOnly;
    private readonly IMaterialsForCollaboratorReadOnly _repositoryMaterialsForCollaboratorReadOnly;
    private readonly IBorrowedMaterialReadOnly _repositoryBorrowedMaterialReadOnly;
    private readonly IBorrowedMaterialWriteOnly _repositoryBorrowedMaterialWriteOnly;
    private readonly IMaterialReadOnlyRepository _repositoryMaterialReadOnly;
    private readonly ILoggedUser _loggedUser;

    public DeleteLoanUseCase(
        IMapper mapper,
        IMaterialsForCollaboratorWriteOnly repositoryMaterialsForCollaboratorWriteOnly, 
        IMaterialsForCollaboratorReadOnly repositoryMaterialsForCollaboratorReadOnly,
        IBorrowedMaterialWriteOnly repositoryBorrowedMaterialWriteOnly,
        IBorrowedMaterialReadOnly repositoryBorrowedMaterialReadOnly,
        IMaterialReadOnlyRepository repositoryMaterialReadOnly,
        ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _repositoryMaterialsForCollaboratorWriteOnly = repositoryMaterialsForCollaboratorWriteOnly;
        _repositoryMaterialsForCollaboratorReadOnly = repositoryMaterialsForCollaboratorReadOnly;
        _repositoryBorrowedMaterialWriteOnly = repositoryBorrowedMaterialWriteOnly;
        _repositoryBorrowedMaterialReadOnly = repositoryBorrowedMaterialReadOnly;
        _repositoryMaterialReadOnly = repositoryMaterialReadOnly;
        _loggedUser = loggedUser;
    }

    public async Task Execute(string hashId)
    {
        var materialForCollaborator =  await ValidateData(hashId);
        var borrowedMaterials = await _repositoryBorrowedMaterialReadOnly.RecoverByHashId(hashId);

        var user = await _loggedUser.RecoveryUser();

        var borrowedMaterialsLog = _mapper.Map<List<BorrowedMaterialDeletionLog>>(borrowedMaterials);

        for ( var i = 0; i < borrowedMaterialsLog.Count; i++) 
        {
            var material = await _repositoryMaterialReadOnly.RecoverByBarCode(borrowedMaterialsLog[i].BarCode);
            borrowedMaterialsLog[i].MaterialName = material.Name;
            borrowedMaterialsLog[i].MaterialDescription = material.Description;
        }

        await _repositoryBorrowedMaterialWriteOnly.RegisterDeletionLog(borrowedMaterialsLog);

        var materialForCollaboratorLog = _mapper.Map<MaterialsForCollaboratorDeletionLog>(materialForCollaborator);
        materialForCollaboratorLog.UserIdDeleted = user.Id;
        
        await _repositoryMaterialsForCollaboratorWriteOnly.RegisterDeletionLog(materialForCollaboratorLog);

        await _repositoryBorrowedMaterialWriteOnly.Delete(hashId);

        await _repositoryMaterialsForCollaboratorWriteOnly.Delete(hashId);
    }

    private async Task<MaterialsForCollaborator> ValidateData(string hashId)
    {
        var materialForCollaborator = await _repositoryMaterialsForCollaboratorReadOnly.RecoverByHashId(hashId) ??
            throw new ExceptionValidationErrors(new List<string> { ErrorMessagesResource.CONCESSAO_NAO_LOCALIZADA });

        if (materialForCollaborator.Confirmed)
        {
            throw new ExceptionValidationErrors(new List<string> { ErrorMessagesResource.EXCLUSAO_CONCESSAO_CONFIRMADA_NAO_PERMITIDA });
        }

        return materialForCollaborator;
    }
}
