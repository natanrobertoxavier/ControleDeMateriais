using AutoMapper;
using ControleDeMateriais.Application.UseCases.Material.Recover;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Exceptions.ExceptionBase;

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

    private static void ValidateData(string enrollment)
    {
        var validator = new RecoverCollaboratorValidator();
        var result = validator.Validate(enrollment);

        if (string.IsNullOrEmpty(enrollment) || string.IsNullOrWhiteSpace(enrollment))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("enrollment", 
                ErrorMessagesResource.MATRICULA_COLABORADOR_EM_BRANCO));
        }

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
