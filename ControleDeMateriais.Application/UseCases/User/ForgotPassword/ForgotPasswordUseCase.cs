using Amazon.Runtime.Internal;
using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Application.UseCases.User.Register;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.User;
using ControleDeMateriais.Domain.Repositories.User.ForgotPassword.Forgot;
using ControleDeMateriais.Domain.Repositories.User.ForgotPassword.RecoveryCode;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.User.ForgotPassword;
public class ForgotPasswordUseCase : IForgotPasswordUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IForgotPasswordSendMailOnlyRepository _forgotPassword;
    private readonly IRecoveryCodeWriteOnlyRepository _recoveryCodeWriteOnly;
    private readonly IRecoveryCodeReadOnlyRepository _recoveryCodeReadOnly;
    private readonly PasswordEncryptor _passwordEncryptor;

    public ForgotPasswordUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IForgotPasswordSendMailOnlyRepository forgotPassword,
        IRecoveryCodeWriteOnlyRepository recoveryCodeWriteOnly,
        IRecoveryCodeReadOnlyRepository recoveryCodeReadOnly,
        PasswordEncryptor passwordEncryptor)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _forgotPassword = forgotPassword;
        _recoveryCodeWriteOnly = recoveryCodeWriteOnly;
        _recoveryCodeReadOnly = recoveryCodeReadOnly;
        _passwordEncryptor = passwordEncryptor;
    }

    public async Task<ResponseForgotPasswordJson> Execute(RequestForgotPasswordJson request)
    {
        var isThereUserWithEmail = await ValidateData(request);

        await _forgotPassword.SendMail(isThereUserWithEmail, await GenerateRecoveryCode(isThereUserWithEmail));

        return new ResponseForgotPasswordJson
        {
            Name = isThereUserWithEmail.Name,
            Email = HiddeEmail(isThereUserWithEmail.Email),
        };
    }

    private async Task<Domain.Entities.User> ValidateData(RequestForgotPasswordJson request)
    {
        var validator = new ForgotPasswordValidator();
        var result = validator.Validate(request);

        var isThereUserWithEmail = await _userReadOnlyRepository.IsThereUserWithEmailReturnUser(request.Email);

        if (isThereUserWithEmail is null)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ErrorMessagesResource.EMAIL_NAO_LOCALIZADO));
        }

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ExceptionValidationErrors(messageError);
        }

        return isThereUserWithEmail;
    }

    private static string HiddeEmail(string email)
    {
        string hiddeDescription = HideCharacters(email[..email.IndexOf('@')]);
        string domain = email[email.IndexOf('@')..];

        return string.Concat(hiddeDescription, domain);
    }

    private static string HideCharacters(string hiddeDescription)
    {
        char firstCharacter = hiddeDescription[0];
        return firstCharacter + new string('*', hiddeDescription.Length - 1);
    }

    private async Task SaveCode(Domain.Entities.User user, string result)
    {
        var isThereCodeActive = await _recoveryCodeReadOnly.IsThereCodeActive(user);

        if (isThereCodeActive is not null)
        {
            await _recoveryCodeWriteOnly.InactivateCode(isThereCodeActive);
        }

        await _recoveryCodeWriteOnly.Add(new RecoveryCode()
        {
            UserId = user._id,
            Code = _passwordEncryptor.Encrypt(result),
            Created = DateTime.UtcNow,
            Active = true,
        });
    }

    private async Task<string> GenerateRecoveryCode(Domain.Entities.User user)
    {
        const string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int lengthCode = 6;
        Random random = new();

        char[] code = new char[lengthCode];

        for (int i = 0; i < lengthCode; i++)
        {
            code[i] = allowedCharacters[random.Next(allowedCharacters.Length)];
        }

        var result = new string(code);

        await SaveCode(user, result);

        return result;
    }
}
