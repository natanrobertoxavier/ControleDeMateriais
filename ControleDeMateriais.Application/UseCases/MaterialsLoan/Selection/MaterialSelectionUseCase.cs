using AutoMapper;
using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Application.UseCases.Material.Recover;
using ControleDeMateriais.Application.UseCases.MaterialsLoan.Recover;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation.Results;
using MongoDB.Bson;
using System.Security.Cryptography;
using System.Text;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Selection;
public class MaterialSelectionUseCase : IMaterialSelectionUseCase
{
    private readonly IRecoverMaterialUseCase _recoverMaterialUseCase;
    private readonly IRecoverBorrowedMaterialUseCase _recoverBorrowedMaterialUseCase;
    private readonly IBorrowedMaterialWriteOnly _repositoryBorrowedMaterialWriteOnly;
    private readonly IMaterialsForCollaboratorWriteOnly _repositoryMaterialsForCollaboratorWriteOnly;
    private readonly ICollaboratorReadOnlyRepository _repositoryCollaboratorReadOnly;
    private readonly ISelectedMaterialsSendMailOnlyRepository _repositoryMaterialsSendMail;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public MaterialSelectionUseCase(
        IRecoverMaterialUseCase recoverMaterialUseCase, 
        IRecoverBorrowedMaterialUseCase recoverBorrowedMaterialUseCase,
        IMaterialsForCollaboratorWriteOnly repositoryMaterialsForCollaboratorWriteOnly,
        IBorrowedMaterialWriteOnly repositoryBorrowedMaterialWriteOnly,
        ICollaboratorReadOnlyRepository repositoryCollaboratorReadOnly,
        ISelectedMaterialsSendMailOnlyRepository repositoryMaterialsSendMail,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _recoverMaterialUseCase = recoverMaterialUseCase;
        _recoverBorrowedMaterialUseCase = recoverBorrowedMaterialUseCase;
        _repositoryBorrowedMaterialWriteOnly = repositoryBorrowedMaterialWriteOnly;
        _repositoryMaterialsForCollaboratorWriteOnly = repositoryMaterialsForCollaboratorWriteOnly;
        _repositoryCollaboratorReadOnly = repositoryCollaboratorReadOnly;
        _repositoryMaterialsSendMail = repositoryMaterialsSendMail;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestMaterialSelectionJson request)
    {
        await ValidateData(request);

        await ValidateLoan(request);

        var user = await _loggedUser.RecoveryUser();

        var entity = new List<BorrowedMaterial>();

        var hashId = GenerateHashId(DateTime.UtcNow.ToString());

        foreach (var barCode in request.BarCode)
        {
            var instancia = new BorrowedMaterial()
            {
                BarCode = barCode,
                HashId = hashId,
                Created = DateTime.UtcNow,
            };

            entity.Add(instancia);
        }

        await _repositoryBorrowedMaterialWriteOnly.Add(entity);

        var materialsForCollaborator = new MaterialsForCollaborator()
        {
            Created = DateTime.UtcNow,
            CollaboratorId = new ObjectId(request.CollaboratorId),
            MaterialsHashId = hashId,
            UserId = user.Id,
        };

        await _repositoryMaterialsForCollaboratorWriteOnly.Add(materialsForCollaborator);

        await SendMail(request, user);
    }

    private async Task ValidateData(RequestMaterialSelectionJson request)
    {
        var messageError = new List<string>();

        foreach (var codeBar in request.BarCode)
        {
            var validator = new RecoverMaterialValidator();
            var result = validator.Validate(codeBar);

            var material = await _recoverMaterialUseCase.Execute(codeBar);

            if (material is null)
            {
                result.Errors.Add(new ValidationFailure("codeBar", ErrorMessagesResource.NENHUM_MATERIAL_LOCALIZADO));
            }

            if (!result.IsValid)
            {
                messageError = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
                throw new ExceptionValidationErrors(messageError);
            }
        }
    }

    private async Task ValidateLoan(RequestMaterialSelectionJson request)
    {
        var entities = request.BarCode.ToList();

        var result = await _recoverBorrowedMaterialUseCase.Execute(entities);

        if (result is not null)
        {
            var validator = new SelectionMaterialValidator();
            var resultValidator = validator.Validate(string.Empty);

            resultValidator.Errors.AddRange(result.Select(item =>
                new ValidationFailure("Material", string.Concat($"{ErrorMessagesResource.MATERIAL_CONCEDIDO_INICIAL} {item} {ErrorMessagesResource.MATERIAL_CONCEDIDO_FINAL}"))));

            throw new ExceptionBorrowedMaterialErros(resultValidator.Errors.Select(error => error.ErrorMessage).ToList());
        }
    }

    private static string GenerateHashId(string input)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            stringBuilder.Append(hashBytes[i].ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    private async Task SendMail(RequestMaterialSelectionJson request, Domain.Entities.User user)
    {
        var entityCollaborator = await _repositoryCollaboratorReadOnly.RecoverById(new ObjectId(request.CollaboratorId));
        var collaborator = _mapper.Map<ResponseCollaboratorJson>(entityCollaborator);

        var materialSendMail = new RequestSelectedMaterialsContentEmailJson
        {
            UserName = user.Name
        };

        var barCode = new List<string>();
        var materialName = new List<string>();
        var materialDescription = new List<string>();


        foreach (var barCodeRequest in request.BarCode)
        {
            var materialDB = await _recoverMaterialUseCase.Execute(barCodeRequest);

            barCode.Add(materialDB.BarCode);
            materialName.Add(materialDB.Name);
            materialDescription.Add(materialDB.Description);
        }

        materialSendMail.BarCode = barCode;
        materialSendMail.MaterialName = materialName;
        materialSendMail.MaterialDescription = materialDescription;

        await _repositoryMaterialsSendMail.SendMail(materialSendMail, collaborator);
    }
}
