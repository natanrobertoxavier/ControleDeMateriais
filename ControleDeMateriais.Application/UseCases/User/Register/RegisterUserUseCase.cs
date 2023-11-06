using AutoMapper;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories;

namespace ControleDeMateriais.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IUserWriteOnlyRepository _repository;

    public RegisterUserUseCase(
        IMapper mapper,
        IUserWriteOnlyRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ResponseUserCreatedJson> Execute(RequestRegisterUserJson request)
    {
        var entity = _mapper.Map<Domain.Entities.User>(request);

        await _repository.Add(entity);

        return new ResponseUserCreatedJson
        {
            Token = string.Empty
        };
    }
}
