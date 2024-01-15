using ControleDeMateriais.Application.UseCases.User.Recover;
using ControleDeMateriais.Communication.Enum;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Domain.Repositories.User;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.Loan.Recover;
public class RecoverMaterialForCollaboratorUseCase : IRecoverMaterialForCollaboratorUseCase
{
    private readonly IBorrowedMaterialReadOnly _repositoryBorrowedMaterialReadOnly;
    private readonly IMaterialReadOnlyRepository _repositoryMaterialReadOnlyRepository;
    private readonly IMaterialsForCollaboratorReadOnly _repositoryMaterialForCollaboratorReadOnly;
    private readonly ICollaboratorReadOnlyRepository _repositoryCollaboratorReadOnly;
    private readonly IUserReadOnlyRepository _repositoryUserReadOnly;
    private readonly IRecoverUserUseCase _userUseCase;

    public RecoverMaterialForCollaboratorUseCase(
        IBorrowedMaterialReadOnly repositoryBorrowedMaterialReadOnly,
        IMaterialReadOnlyRepository repositoryMaterialReadOnlyRepository,
        IMaterialsForCollaboratorReadOnly repositoryMaterialForCollaboratorReadOnly,
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

    public async Task<List<ResponseMaterialForCollaboratorJson>> Execute()
    {
        var materialsForCollaborator = await _repositoryMaterialForCollaboratorReadOnly.RecoverAll();

        return await AddMaterialsForCollaboratorInformation(materialsForCollaborator);
    }

    public async Task<List<ResponseMaterialForCollaboratorJson>> Execute(string enrollment, bool status)
    {
        var collaborator = await _repositoryCollaboratorReadOnly.RecoverByEnrollment(enrollment) ??
            throw new ExceptionValidationErrors(new List<string> { ErrorMessagesResource.COLABORADOR_NAO_LOCALIZADO });

        var materialsForCollaborator = await _repositoryMaterialForCollaboratorReadOnly.RecoverByCollaboratorLoanStatus(collaborator.Id, status);

        return await AddMaterialsForCollaboratorInformation(materialsForCollaborator);
    }

    public async Task<List<ResponseMaterialForCollaboratorJson>> Execute(bool status)
    {
        var materialsForCollaborator = await _repositoryMaterialForCollaboratorReadOnly.RecoverByStatus(status);

        return await AddMaterialsForCollaboratorInformation(materialsForCollaborator);
    }

    public async Task<List<ResponseMaterialForCollaboratorJson>> Execute(string enrollment)
    {
        var collaborator = await _repositoryCollaboratorReadOnly.RecoverByEnrollment(enrollment) ??
            throw new ExceptionValidationErrors(new List<string> { ErrorMessagesResource.COLABORADOR_NAO_LOCALIZADO });

        var materialsForCollaborator = await _repositoryMaterialForCollaboratorReadOnly.RecoverByCollaborator(collaborator.Id);

        return await AddMaterialsForCollaboratorInformation(materialsForCollaborator);
    }

    public async Task<List<ResponseMaterialForCollaboratorJson>> Execute(DateTime? initial, DateTime? final)
    {
        if (initial.HasValue)
        {
            initial = initial.Value.Date;
        }

        if (final.HasValue)
        {
            final = final.Value.Date.AddDays(1).AddTicks(-1);
        }

        ValidateDates(initial, final);

        var materialsForCollaborator = await _repositoryMaterialForCollaboratorReadOnly.RecoverByDateInitialFinal(initial, final);

        return await AddMaterialsForCollaboratorInformation(materialsForCollaborator);
    }

    private async Task<List<ResponseMaterialForCollaboratorJson>> AddMaterialsForCollaboratorInformation(List<MaterialsForCollaborator> materialsForCollaborator)
    {
        var result = new List<ResponseMaterialForCollaboratorJson>();

        foreach (var materialForCollaborator in materialsForCollaborator)
        {
            var resultBorrowedMaterials = await _repositoryBorrowedMaterialReadOnly
                .RecoverByHashId(materialForCollaborator.MaterialsHashId);

            var resultListMaterials = await AddMaterialInformation(resultBorrowedMaterials);

            var collaborator = await _repositoryCollaboratorReadOnly.RecoverById(materialForCollaborator.CollaboratorId);
            var collaboratorConfirm = await _repositoryCollaboratorReadOnly.RecoverById(materialForCollaborator.CollaboratorConfirmedId);
            var user = await _repositoryUserReadOnly.RecoverById(materialForCollaborator.UserId);

            result.Add(new ResponseMaterialForCollaboratorJson
            {
                CollaboratorEnrollment = collaborator?.Enrollment,
                CollaboratorNickname = collaborator?.Nickname,
                CollaboratorTelephone = collaborator?.Telephone,
                UserNameRegisterLoan = user?.Name,
                LoanConfirmed = materialForCollaborator.Confirmed,
                LoanDateTime = materialForCollaborator.DateTimeConfirmation,
                ColaboratorNicknameConfirmed = collaboratorConfirm?.Nickname,
                ListMaterialBorrowed = resultListMaterials,
                HashIdLoan = materialForCollaborator.MaterialsHashId,
            });
        }

        return result;
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
            var category = (Category)materialDBs[material.BarCode].Category;

            var categoryName = category.GetDescription();

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

    private async Task<List<ResponseBorrowedListMaterialJson>> AddMaterialInformation(BorrowedMaterial resultBorrowedMaterials)
    {
        var resultListMaterials = new List<ResponseBorrowedListMaterialJson>();
        var materialDB = await _repositoryMaterialReadOnlyRepository.RecoverByBarCode(resultBorrowedMaterials.BarCode);

        var category = (Category)materialDB.Category;
        var categoryName = category.GetDescription();

        var user = await _userUseCase.Execute(resultBorrowedMaterials.UserReceivedId.ToString()) ?? new ResponseUserJson();

        resultListMaterials.Add(new ResponseBorrowedListMaterialJson
        {
            MaterialName = materialDB.Name,
            MaterialDescription = materialDB.Description,
            BarCode = resultBorrowedMaterials.BarCode,
            CategoryName = categoryName,
            UserReceivedName = user.Name,
            DateReceived = resultBorrowedMaterials.DateReceived,
        });

        return resultListMaterials;
    }

    private static void ValidateDates(DateTime? initial, DateTime? final)
    {
        if (initial > final)
        {
            throw new ExceptionValidationErrors(new List<string> { ErrorMessagesResource.DATA_FINAL_MAIOR_INICIAL });
        }
    }

}
