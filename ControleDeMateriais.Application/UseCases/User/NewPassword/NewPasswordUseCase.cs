using AutoMapper;
using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Application.Services.Token;
using ControleDeMateriais.Application.UseCases.User.Register;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.User;
using ControleDeMateriais.Domain.Repositories.User.ForgotPassword.RecoveryCode;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.User.NewPassword;
public class NewPasswordUseCase : INewPasswordUseCase
{
    private readonly IRecoveryCodeReadOnlyRepository _recoveryCodeReadOnly;
    private readonly IUserReadOnlyRepository _userRepositoryReadOnly;
    private readonly IUserWriteOnlyRepository _userRepositoryWriteOnly;
    private readonly PasswordEncryptor _passwordEncryptor;
    private readonly TokenController _tokenController;

    public NewPasswordUseCase(
        IRecoveryCodeReadOnlyRepository recoveryCodeReadOnly,
        IUserReadOnlyRepository repositoryReadOnly,
        IUserWriteOnlyRepository repositoryWriteOnly,
        PasswordEncryptor passwordEncryptor,
        TokenController tokenController)
    {
        _recoveryCodeReadOnly = recoveryCodeReadOnly;
        _userRepositoryReadOnly = repositoryReadOnly;
        _userRepositoryWriteOnly = repositoryWriteOnly;
        _passwordEncryptor = passwordEncryptor;
        _tokenController = tokenController;
    }
    public async Task<ResponseNewPasswordJson> Execute(RequestNewPasswordJson request)
    {
        var user = await _userRepositoryReadOnly.IsThereUserWithEmailReturnUser(request.Email);
        var codeRecovery = await _recoveryCodeReadOnly.IsThereCodeActive(user);
        var codeRequest = _passwordEncryptor.Encrypt(request.RecoveryCode);

        await ValidateData(request, codeRequest, codeRecovery);

        user.Password = _passwordEncryptor.Encrypt(request.NewPassword);

        await _userRepositoryWriteOnly.UpdatePassword(user);

        return new ResponseNewPasswordJson
        {
            Name = user.Name,
            Token = _tokenController.TokenGenerate(user.Email)
        };
    }
    

    private async Task ValidateData(
        RequestNewPasswordJson request, 
        string codeRequest,
        Domain.Entities.RecoveryCode codeRecovery)
    {
        var validator = new NewPasswordValidator();
        var result = validator.Validate(request);

        if (codeRequest != codeRecovery.Code)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("code", ErrorMessagesResource.CODIGOS_NAO_CONFEREM));

        if (codeRecovery.Created.AddMinutes(30) < DateTime.UtcNow)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("code", ErrorMessagesResource.CODIGO_EXPIRADO));

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
