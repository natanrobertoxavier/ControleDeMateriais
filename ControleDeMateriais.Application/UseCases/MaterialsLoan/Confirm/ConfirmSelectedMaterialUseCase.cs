using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Application.UseCases.Collaborator.ConfirmPassword;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
using ControleDeMateriais.Domain.Repositories.Loan.MaterialForCollaborator;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Confirm;
public class ConfirmSelectedMaterialUseCase : IConfirmSelectedMaterialUseCase
{
    private readonly IMaterialsForCollaboratorWriteOnly _repositoryMaterialsForCollaboratorWriteOnly;
    private readonly IMaterialForCollaboratorReadOnly _repositoryMaterialForCollaboratorReadOnly;
    private readonly IBorrowedMaterialWriteOnly _repositoryMaterialWriteOnly;
    private readonly IConfirmPasswordUseCase _confirmPasswordCollaboratorUseCase;
    private readonly PasswordEncryptor _passwordEncryptor;

    public ConfirmSelectedMaterialUseCase(
        IMaterialsForCollaboratorWriteOnly repositoryMaterialsForCollaboratorWriteOnly,
        IMaterialForCollaboratorReadOnly repositoryMaterialForCollaboratorReadOnly,
        IBorrowedMaterialWriteOnly repositoryMaterialWriteOnly,
        IConfirmPasswordUseCase confirmPasswordCollaboratorUseCase,
        PasswordEncryptor passwordEncryptor)
    {
        _repositoryMaterialsForCollaboratorWriteOnly = repositoryMaterialsForCollaboratorWriteOnly;
        _repositoryMaterialForCollaboratorReadOnly = repositoryMaterialForCollaboratorReadOnly;
        _repositoryMaterialWriteOnly = repositoryMaterialWriteOnly;
        _confirmPasswordCollaboratorUseCase = confirmPasswordCollaboratorUseCase;
        _passwordEncryptor = passwordEncryptor;
    }

    public async Task Execute(RequestConfirmSelectedMaterialJson request)
    {
        var collaborator = await ValidateData(request);

        await _repositoryMaterialsForCollaboratorWriteOnly.Confirm(request.HashId, collaborator.Id);
        await _repositoryMaterialWriteOnly.Confirm(request.HashId);
    }

    private async Task<ResponseCollaboratorJson> ValidateData(RequestConfirmSelectedMaterialJson request)
    {
        _ = await _repositoryMaterialForCollaboratorReadOnly.RecoverByHashId(request.HashId) ??
            throw new ExceptionValidationErrors(new List<string> { ErrorMessagesResource.CONCESSAO_NAO_LOCALIZADA });

        var encryptedPassword = _passwordEncryptor.Encrypt(request.Password);

        var collaborator = await _confirmPasswordCollaboratorUseCase.Execute(request.Enrollment, encryptedPassword);

        return collaborator;
    }
}
