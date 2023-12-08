using AutoMapper;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Collaborator;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Recover;
public class RecoverCollaboratorUserCase : IRecoverCollaboratorUserCase
{
    private readonly IMapper _mapper;
    private readonly ICollaboratorReadOnlyRepository _repositoryCollaboratorReadOnly;

    public RecoverCollaboratorUserCase(
        IMapper mapper, 
        ICollaboratorReadOnlyRepository repositoryCollaboratorReadOnly)
    {
        _mapper = mapper;
        _repositoryCollaboratorReadOnly = repositoryCollaboratorReadOnly;
    }

    public async Task<List<ResponseCollaboratorJson>> Execute()
    {
        var collaborator = await _repositoryCollaboratorReadOnly.RecoverAll();

        return _mapper.Map<List<ResponseCollaboratorJson>>(collaborator);
    }

    public async Task<ResponseCollaboratorJson> Execute(string enrollment)
    {
        ValidateData(enrollment);

        var collaborator = await _repositoryCollaboratorReadOnly.RecoverByEnrollment(enrollment);

        return _mapper.Map<ResponseCollaboratorJson>(collaborator);
    }

    private void ValidateData(string enrollment)
    {
        //Implementar a validação do número da matrícula
        throw new NotImplementedException();
    }
}
