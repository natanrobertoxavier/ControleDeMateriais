using AutoMapper;
using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories;
using ControleDeMateriais.Exceptions.ExceptionBase;
using Microsoft.Extensions.Options;

namespace ControleDeMateriais.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IUserWriteOnlyRepository _repository;
    private readonly PasswordEncryptor _passwordEncryptor;

    public RegisterUserUseCase(
        IMapper mapper,
        IUserWriteOnlyRepository repository,
        PasswordEncryptor passwordEncryptor)
    {
        _mapper = mapper;
        _repository = repository;
        _passwordEncryptor = passwordEncryptor;
    }

    public async Task<ResponseUserCreatedJson> Execute(RequestRegisterUserJson request)
    {
        await ValidateData(request);
        var entity = _mapper.Map<Domain.Entities.User>(request);
        entity.Password = _passwordEncryptor.Encrypt(request.Password);

        await _repository.Add(entity);

        return new ResponseUserCreatedJson
        {
            Token = string.Empty
        };
    }

    private async Task ValidateData(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
