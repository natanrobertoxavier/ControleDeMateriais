using AutoMapper;
using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Application.UseCases.Material.Recover;
using ControleDeMateriais.Application.UseCases.MaterialsLoan.Recover;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using ControleDeMateriais.Domain.Repositories.Loan.Register;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation;
using FluentValidation.Results;
using MongoDB.Bson;
using System.Security.Cryptography;
using System.Text;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Selection;
public class MaterialSelectionUseCase : IMaterialSelectionUseCase
{
    private readonly IMapper _mapper;
    private readonly IRecoverMaterialUseCase _recoverMaterialUseCase;
    private readonly IRecoverBorrowedMaterialUseCase _recoverBorrowedMaterialUseCase;
    private readonly IBorrowedMaterialWriteOnly _repositoryBorrowedMaterialWriteOnly;
    private readonly IMaterialsForCollaboratorWriteOnly _repositoryMaterialsForCollaboratorWriteOnly;
    private readonly ILoggedUser _loggedUser;

    public MaterialSelectionUseCase(
        IMapper mapper, 
        IRecoverMaterialUseCase recoverMaterialUseCase, 
        IRecoverBorrowedMaterialUseCase recoverBorrowedMaterialUseCase,
        IMaterialsForCollaboratorWriteOnly repositoryMaterialsForCollaboratorWriteOnly,
        IBorrowedMaterialWriteOnly repositoryBorrowedMaterialWriteOnly,

        ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _recoverMaterialUseCase = recoverMaterialUseCase;
        _recoverBorrowedMaterialUseCase = recoverBorrowedMaterialUseCase;
        _repositoryBorrowedMaterialWriteOnly = repositoryBorrowedMaterialWriteOnly;
        _repositoryMaterialsForCollaboratorWriteOnly = repositoryMaterialsForCollaboratorWriteOnly;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestMaterialSelectionJson request)
    {
        await ValidateData(request);

        await ValidateLoan(request);

        var user = await _loggedUser.RecoveryUser();

        var entity = new List<BorrowedMaterial>();

        var hashId = GenerateHashId(DateTime.UtcNow.ToString());

        foreach (var codeBar in request.CodeBar)
        {
            var instancia = new BorrowedMaterial()
            {
                Active = true,
                CodeBar = codeBar,
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

        await _repositoryMaterialsForCollaboratorWriteOnly.Register(materialsForCollaborator);

        //Aqui vai a lógica para o envio de e-mail com a descrição dos materiais tomados de empréstimo
    }

    private async Task ValidateData(RequestMaterialSelectionJson request)
    {
        var messageError = new List<string>();

        foreach (var codeBar in request.CodeBar)
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
        var entities = request.CodeBar.ToList();

        var result = await _recoverBorrowedMaterialUseCase.Execute(entities);

        if (result is not null)
        {
            var validator = new SelectionMaterialValidator();
            var resultValidator = validator.Validate(string.Empty);

            resultValidator.Errors.AddRange(result.Select(item =>
                new ValidationFailure("Material", $"O material cadastrado sob o código de barras {item} encontra-se emprestado.")));

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
}
