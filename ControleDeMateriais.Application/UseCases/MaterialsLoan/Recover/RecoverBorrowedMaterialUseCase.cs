﻿using ControleDeMateriais.Application.UseCases.User.Recover;
using ControleDeMateriais.Communication.Enum;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Domain.Repositories.User;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Recover;
public class RecoverBorrowedMaterialUseCase : IRecoverBorrowedMaterialUseCase
{
    private readonly IBorrowedMaterialReadOnly _repositoryBorrowedMaterialReadOnly;
    private readonly IMaterialReadOnlyRepository _repositoryMaterialReadOnlyRepository;
    private readonly IMaterialForCollaboratorReadOnly _repositoryMaterialForCollaboratorReadOnly;
    private readonly ICollaboratorReadOnlyRepository _repositoryCollaboratorReadOnly;
    private readonly IUserReadOnlyRepository _repositoryUserReadOnly;
    private readonly IRecoverUserUseCase _userUseCase;

    public RecoverBorrowedMaterialUseCase(
        IBorrowedMaterialReadOnly repositoryBorrowedMaterialReadOnly,
        IMaterialReadOnlyRepository repositoryMaterialReadOnlyRepository,
        IMaterialForCollaboratorReadOnly repositoryMaterialForCollaboratorReadOnly,
        ICollaboratorReadOnlyRepository repositoryCollaboratorReadOnly,
        IUserReadOnlyRepository repositoryUserReadOnly,
        IRecoverUserUseCase userUseCase)
    {
        _repositoryBorrowedMaterialReadOnly = repositoryBorrowedMaterialReadOnly;
        _repositoryMaterialReadOnlyRepository = repositoryMaterialReadOnlyRepository;
        _repositoryMaterialForCollaboratorReadOnly = repositoryMaterialForCollaboratorReadOnly;
        _repositoryCollaboratorReadOnly = repositoryCollaboratorReadOnly;
        _repositoryUserReadOnly = repositoryUserReadOnly;
        _userUseCase = userUseCase;
    }

    public async Task<List<ResponseBorrowedMaterialJson>> Execute()
    {
        var result = new List<ResponseBorrowedMaterialJson>();

        var materialsForCollaborator = await _repositoryMaterialForCollaboratorReadOnly.RecoverAll();

        foreach (var materialForCollaborator in materialsForCollaborator)
        {
            var resultBorrowedMaterials = await _repositoryBorrowedMaterialReadOnly
                .RecoverForHashId(materialForCollaborator.MaterialsHashId);

            var resultListMaterials = await AddMaterialInformation(resultBorrowedMaterials);

            var collaborator = await _repositoryCollaboratorReadOnly.RecoverById(materialForCollaborator.CollaboratorId);
            var collaboratorConfirm = await _repositoryCollaboratorReadOnly.RecoverById(materialForCollaborator.UserIdConfirmed);
            var user = await _repositoryUserReadOnly.RecoverById(materialForCollaborator.UserId);

            result.Add(new ResponseBorrowedMaterialJson
            {
                CollaboratorEnrollment = collaborator?.Enrollment,
                CollaboratorNickname = collaborator?.Nickname,
                CollaboratorTelephone = collaborator?.Telephone,
                UserNameRegisterLoan = user?.Name,
                LoanConfirmed = materialForCollaborator.Confirmed,
                LoanDateTime = materialForCollaborator.DateTimeConfirmation,
                ColaboratorNicknameConfirmed = collaboratorConfirm?.Nickname,
                ListMaterialBorrowed = resultListMaterials,
            });
        }

        return result;
    }

    public async Task<List<string>> Execute(List<string> codeBar)
    {
        return await _repositoryBorrowedMaterialReadOnly.RecoverBorrowedMaterial(codeBar);
    }

    private async Task<List<ResponseBorrowedListMaterialJson>> AddMaterialInformation(List<BorrowedMaterial> resultBorrowedMaterials)
    {
        var resultListMaterials = new List<ResponseBorrowedListMaterialJson>();
        var materialDBs = new Dictionary<string, Domain.Entities.Material>();

        foreach (var material in resultBorrowedMaterials)
        {
            var materialDB = await _repositoryMaterialReadOnlyRepository.RecoverByBarCode(material.BarCode);
            materialDBs[material.BarCode] = materialDB;
        }

        foreach (var material in resultBorrowedMaterials)
        {
            var category = (Category) materialDBs[material.BarCode].Category;

            var categoryName = EnumExtensions.GetDescription(category);

            var user = await _userUseCase.Execute(material.UserReceivedId.ToString()) ?? new ResponseUserJson();

            resultListMaterials.Add(new ResponseBorrowedListMaterialJson
            {
                MaterialName = materialDBs[material.BarCode].Name,
                MaterialDescription = materialDBs[material.BarCode].Description,
                BarCode = material.BarCode,
                CategoryName = categoryName,
                UserReceivedName = user.Name,
                DateReceived = material.DateReceived,
            });
        }

        return resultListMaterials;
    }
}
