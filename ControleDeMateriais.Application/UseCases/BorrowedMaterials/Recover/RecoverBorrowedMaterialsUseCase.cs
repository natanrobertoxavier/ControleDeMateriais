using AutoMapper;
using ControleDeMateriais.Communication.Enum;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Domain.Repositories.User;

namespace ControleDeMateriais.Application.UseCases.BorrowedMaterials.Recover;
public class RecoverBorrowedMaterialsUseCase : IRecoverBorrowedMaterialsUseCase
{
    private readonly IMapper _mapper;
    private readonly IBorrowedMaterialReadOnly _repositoryBorrowedMaterialReadOnly;
    private readonly IMaterialsForCollaboratorReadOnly _repositoryMaterialsForCollaboratorReadOnly;
    private readonly IMaterialReadOnlyRepository _repositoryMaterialReadOnlyRepository;
    private readonly ICollaboratorReadOnlyRepository _repositoryCollaboratorReadOnly;
    private readonly IUserReadOnlyRepository _repositoryUserReadOnly;

    public RecoverBorrowedMaterialsUseCase(
        IMapper mapper,
        IBorrowedMaterialReadOnly repositoryBorrowedMaterialReadOnly,
        IMaterialsForCollaboratorReadOnly repositoryMaterialsForCollaboratorWriteOnly,
        IMaterialReadOnlyRepository repositoryMaterialReadOnlyRepository,
        ICollaboratorReadOnlyRepository repositoryCollaboratorReadOnly,
        IUserReadOnlyRepository repositoryUserReadOnly)
    {
        _mapper = mapper;
        _repositoryBorrowedMaterialReadOnly = repositoryBorrowedMaterialReadOnly;
        _repositoryMaterialsForCollaboratorReadOnly = repositoryMaterialsForCollaboratorWriteOnly;
        _repositoryMaterialReadOnlyRepository = repositoryMaterialReadOnlyRepository;
        _repositoryCollaboratorReadOnly = repositoryCollaboratorReadOnly;
        _repositoryUserReadOnly = repositoryUserReadOnly;
    }

    public async Task<List<ResponseBorrowedMaterialJson>> Execute()
    {
        var result = new List<ResponseBorrowedMaterialJson>();

        var resultBorrowedMaterials = await _repositoryBorrowedMaterialReadOnly.RecoverAll();

        foreach (var item in resultBorrowedMaterials)
        {
            var materialForCollaborator = await _repositoryMaterialsForCollaboratorReadOnly.RecoverByHashId(item.HashId);
            var collaborator = await _repositoryCollaboratorReadOnly.RecoverById(materialForCollaborator.CollaboratorId);
            var material = await _repositoryMaterialReadOnlyRepository.RecoverByBarCode(item.BarCode);

            var userReceived = await _repositoryUserReadOnly.RecoverById(item.UserReceivedId);

            var category = (Category) material.Category;

            var categoryName = category.GetDescription();

            var borrowedMaterial = _mapper.Map<ResponseBorrowedMaterialJson>(item);

            borrowedMaterial.MaterialName = material.Name;
            borrowedMaterial.MaterialDescription = material.Description;
            borrowedMaterial.CategoryName = categoryName;
            borrowedMaterial.CollaboratorEnrollment = collaborator.Enrollment;
            borrowedMaterial.CollaboratorNickName = collaborator.Nickname;
            borrowedMaterial.CollaboratorTelephone = collaborator.Telephone;
            borrowedMaterial.UserReceivedName = userReceived.Name;

            result.Add(borrowedMaterial);
        }

        return result;
    }

    public async Task<List<string>> Execute(List<string> codeBar)
    {
        return await _repositoryBorrowedMaterialReadOnly.RecoverBorrowedMaterial(codeBar);
    }
}
