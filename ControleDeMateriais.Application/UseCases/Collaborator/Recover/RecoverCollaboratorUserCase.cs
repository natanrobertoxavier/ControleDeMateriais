using AutoMapper;
using ControleDeMateriais.Application.UseCases.User.Recover;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Recover;
public class RecoverCollaboratorUserCase : IRecoverCollaboratorUserCase
{
    private readonly IMapper _mapper;
    private readonly ICollaboratorReadOnlyRepository _repositoryCollaboratorReadOnly;
    private readonly IRecoverUserUseCase _repositoryUserReadOnly;

    public RecoverCollaboratorUserCase(
        IMapper mapper,
        ICollaboratorReadOnlyRepository repositoryCollaboratorReadOnly,
        IRecoverUserUseCase repositoryUserReadOnly)
    {
        _mapper = mapper;
        _repositoryCollaboratorReadOnly = repositoryCollaboratorReadOnly;
        _repositoryUserReadOnly = repositoryUserReadOnly;
    }

    public async Task<List<ResponseCollaboratorJson>> Execute()
    {
        var collaborator = await _repositoryCollaboratorReadOnly.RecoverAll();
        var result = await GetNameUserCreated(collaborator);

        return result;
    }

    public async Task<ResponseCollaboratorJson> Execute(string enrollment)
    {
        ValidateData(enrollment);

        var collaborator = await _repositoryCollaboratorReadOnly.RecoverByEnrollment(enrollment);

        var user = await _repositoryUserReadOnly.Execute(collaborator.UserIdCreated.ToString());

        var result = _mapper.Map<ResponseCollaboratorJson>(collaborator);

        result.UserNameCreated = user.Name;

        return result;
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

    private async Task<List<ResponseCollaboratorJson>> GetNameUserCreated(List<Domain.Entities.Collaborator> Collaborators)
    {
        var result = new List<ResponseCollaboratorJson>();
        foreach (var collaborator in Collaborators)
        {
            var user = await _repositoryUserReadOnly.Execute(collaborator.UserIdCreated.ToString());

            result.Add(new ResponseCollaboratorJson()
            {
                Name = collaborator.Name,
                Nickname = collaborator.Nickname,
                Enrollment = collaborator.Enrollment,
                Email = collaborator.Email,
                Telephone = collaborator.Telephone,
                UserNameCreated = user.Name,
            });
        }

        return result;
    }
}
