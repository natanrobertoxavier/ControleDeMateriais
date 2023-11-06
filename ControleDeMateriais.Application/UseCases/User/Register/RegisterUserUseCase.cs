using AutoMapper;
using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories;

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
        var entity = _mapper.Map<Domain.Entities.User>(request);
        entity.Password = _passwordEncryptor.Encrypt(request.Password);

        await _repository.Add(entity);

        return new ResponseUserCreatedJson
        {
            Token = string.Empty
        };
    }
}
