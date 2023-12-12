using AutoMapper;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.Collaborator.ConfirmPassword;
public class ConfirmPasswordUseCase : IConfirmPasswordUseCase
{
    private readonly IMapper _mapper;
    private readonly ICollaboratorReadOnlyRepository _repositoryCollaboratorReadOnly;

    public ConfirmPasswordUseCase(
        IMapper mapper,
        ICollaboratorReadOnlyRepository repositoryCollaboratorReadOnly)
    {
        _mapper = mapper;
        _repositoryCollaboratorReadOnly = repositoryCollaboratorReadOnly;
    }

    public async Task<ResponseCollaboratorJson> Execute(string enrollment, string password)
    {
        var user = await _repositoryCollaboratorReadOnly.ConfirmPassword(enrollment, password) ??
            throw new ExceptionLoginInvalid();

        return _mapper.Map<ResponseCollaboratorJson>(user);
    }
}
