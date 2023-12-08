using AutoMapper;
using ControleDeMateriais.Application.UseCases.User.Recover;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Exceptions.ExceptionBase;
using MongoDB.Driver;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Recover;
public class RecoverCollaboratorUseCase : IRecoverCollaboratorUseCase
{
    private readonly IMapper _mapper;
    private readonly ICollaboratorReadOnlyRepository _repositoryCollaboratorReadOnly;
    private readonly IRecoverUserUseCase _repositoryUserReadOnly;

    public RecoverCollaboratorUseCase(
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
        var result = _mapper.Map<ResponseCollaboratorJson>(collaborator);

        if (collaborator is not null)
        {
            var user = await _repositoryUserReadOnly.Execute(collaborator.UserIdCreated.ToString());
            result.UserNameCreated = user.Name;
        }

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
            var user = await _repositoryUserReadOnly.Execute(collaborator.UserIdCreated.ToString()) ?? new ResponseUserJson();

            result.Add(new ResponseCollaboratorJson()
            {
                Id = collaborator.Id.ToString(),
                Created = collaborator.Created,
                Name = collaborator.Name,
                Nickname = collaborator.Nickname,
                Enrollment = collaborator.Enrollment,
                Email = collaborator.Email,
                Telephone = collaborator.Telephone,
                UserIdCreated = collaborator.UserIdCreated.ToString(),
                UserNameCreated = user.Name,
            });
        }

        return result;
    }
}
