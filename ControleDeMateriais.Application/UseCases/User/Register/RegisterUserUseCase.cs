using AutoMapper;
using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Application.Services.Token;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.User;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IUserWriteOnlyRepository _userRepositoryWriteOnly;
    private readonly IUserReadOnlyRepository _userRepositoryReadOnly;
    private readonly PasswordEncryptor _passwordEncryptor;
    private readonly TokenController _tokenController;

    public RegisterUserUseCase(
        IMapper mapper,
        IUserWriteOnlyRepository repositoryWriteOnly,
        IUserReadOnlyRepository repositoryReadOnly,
        PasswordEncryptor passwordEncryptor,
        TokenController tokenController)
    {
        _mapper = mapper;
        _userRepositoryWriteOnly = repositoryWriteOnly;
        _userRepositoryReadOnly = repositoryReadOnly;
        _passwordEncryptor = passwordEncryptor;
        _tokenController = tokenController;
    }

    public async Task<ResponseUserCreatedJson> Execute(RequestRegisterUserJson request)
    {
        await ValidateData(request);
        var entity = _mapper.Map<Domain.Entities.User>(request);
        entity.Password = _passwordEncryptor.Encrypt(request.Password);

        await _userRepositoryWriteOnly.Add(entity);

        var token = _tokenController.TokenGenerate(entity.Email);

        return new ResponseUserCreatedJson
        {
            Token = token
        };
    }

    private async Task ValidateData(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var isThereUserWithEmail = await _userRepositoryReadOnly.IsThereUserWithEmail(request.Email);
        var isThereUserWithCpf = await _userRepositoryReadOnly.IsThereUserWithCpf(request.Cpf);

        if (isThereUserWithEmail)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ErrorMessagesResource.EMAIL_CADASTRADO));
        }

        if (isThereUserWithCpf)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("cpf", ErrorMessagesResource.CPF_CADASTRADO));
        }

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
