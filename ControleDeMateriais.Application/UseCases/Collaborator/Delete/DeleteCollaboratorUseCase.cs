using AutoMapper;
using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Application.UseCases.Collaborator.Recover;
using ControleDeMateriais.Application.UseCases.Material.Recover;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation.Results;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Delete;
public class DeleteCollaboratorUseCase : IDeleteCollaboratorUseCase
{
    private readonly IMapper _mapper;
    private readonly IRecoverCollaboratorUseCase _recoverCollaboratorUseCase;
    private readonly ICollaboratorWriteOnlyRepository _repositoryCollaboratorWriteOnly;
    private readonly ILoggedUser _loggedUser;

    public DeleteCollaboratorUseCase(
        IMapper mapper, 
        IRecoverCollaboratorUseCase recoverCollaboratorUseCase, 
        ICollaboratorWriteOnlyRepository repositoryCollaboratorWriteOnly, 
        ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _recoverCollaboratorUseCase = recoverCollaboratorUseCase;
        _repositoryCollaboratorWriteOnly = repositoryCollaboratorWriteOnly;
        _loggedUser = loggedUser;
    }

    public async Task Execute(string enrollment)
    {
        var collaborator = await ValidateData(enrollment);
        var user = await _loggedUser.RecoveryUser();

        var collaboratorLog = _mapper.Map<Domain.Entities.CollaboratorDeletionLog>(collaborator);
        collaboratorLog.UserIdDeleted = user.Id;
        await _repositoryCollaboratorWriteOnly.RegisterDeletionLog(collaboratorLog);

        await _repositoryCollaboratorWriteOnly.Delete(enrollment);
    }

    private async Task<Domain.Entities.Collaborator> ValidateData(string enrollment)
    {
        var validator = new RecoverCollaboratorValidator();
        var resultValidation = validator.Validate(enrollment);
        var collaborator = await _recoverCollaboratorUseCase.Execute(enrollment);

        if (collaborator is null)
        {
            resultValidation.Errors.Add(new ValidationFailure("Collaborator", ErrorMessagesResource.COLABORADOR_NAO_LOCALIZADO));
        }

        if (!resultValidation.IsValid)
        {
            var messageError = resultValidation.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
            throw new ExceptionValidationErrors(messageError);
        }

        var result = _mapper.Map<Domain.Entities.Collaborator>(collaborator);

        return result;
    }
}
