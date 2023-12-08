using AutoMapper;
using ControleDeMateriais.Application.UseCases.Material.Update;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation.Results;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Update;
public class UpdateCollaboratorUseCase : IUpdateCollaboratorUseCase
{
    private readonly IMapper _mapper;
    private readonly ICollaboratorWriteOnlyRepository _repositoryCollaboratorWriteOnly;
    private readonly ICollaboratorReadOnlyRepository _repositoryCollaboratorReadOnly;

    public UpdateCollaboratorUseCase(
        IMapper mapper, 
        ICollaboratorWriteOnlyRepository repositoryCollaboratorWriteOnly, 
        ICollaboratorReadOnlyRepository repositoryCollaboratorReadOnly)
    {
        _mapper = mapper;
        _repositoryCollaboratorWriteOnly = repositoryCollaboratorWriteOnly;
        _repositoryCollaboratorReadOnly = repositoryCollaboratorReadOnly;
    }

    public async Task<ResponseCollaboratorJson> Execute(string enrollment, RequestUpdateCollaboratorJson request)
    {
        await ValidateData(enrollment, request);

        var entity = _mapper.Map<Domain.Entities.Collaborator>(request);

        var result = await _repositoryCollaboratorWriteOnly.Update(enrollment, entity);

        return _mapper.Map<ResponseCollaboratorJson>(request);
    }

    private async Task ValidateData(string enrollment, RequestUpdateCollaboratorJson request)
    {
        var enrollmentDB = await _repositoryCollaboratorReadOnly.RecoverByEnrollment(enrollment);

        var validator = new CollaboratorUpdateValidator();
        var result = validator.Validate(request);

        if (enrollmentDB is null)
        {
            result.Errors.Add(new ValidationFailure("Enrollment", ErrorMessagesResource.COLABORADOR_NAO_LOCALIZADO));
        }

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
