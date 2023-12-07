using AutoMapper;
using ControleDeMateriais.Application.Services.Cryptography;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Collaborator;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Register;
public class RegisterCollaboratorUseCase : IRegisterCollaboratorUseCase
{
    private readonly IMapper _mapper;
    private readonly ICollaboratorWriteOnlyRepository _collaboratorRepositoryWriteOnly;
    private readonly ICollaboratorReadOnlyRepository _collaboratorRepositoryReadOnly;
    private readonly PasswordEncryptor _passwordEncryptor;

    public RegisterCollaboratorUseCase(
        IMapper mapper,
        ICollaboratorWriteOnlyRepository collaboratorRepositoryWriteOnly,
        ICollaboratorReadOnlyRepository collaboratorRepositoryReadOnly, 
        PasswordEncryptor passwordEncryptor)
    {
        _mapper = mapper;
        _collaboratorRepositoryWriteOnly = collaboratorRepositoryWriteOnly;
        _collaboratorRepositoryReadOnly = collaboratorRepositoryReadOnly;
        _passwordEncryptor = passwordEncryptor;
    }

    public async Task<ResponseCollaboratorCreatedJson> Execute(RequestCollaboratorJson request)
    {
        await ValidateData(request);

        var entity = _mapper.Map<Domain.Entities.Collaborator>(request);
        entity.Password = _passwordEncryptor.Encrypt(request.Password);
        await _collaboratorRepositoryWriteOnly.Add(entity);

        return new ResponseCollaboratorCreatedJson
        {
            Name = request.Name,
        };
    }

    private async Task ValidateData(RequestCollaboratorJson request)
    {

        var validator = new RegisterCollaboratorValidator();
        var result = validator.Validate(request);


        var isThereUserWithEmail = await _collaboratorRepositoryReadOnly.IsThereUserWithEmail(request.Email);
        var isThereUserWithCpf = await _collaboratorRepositoryReadOnly.IsThereUserWithCpf(request.Cpf);

        if (isThereUserWithEmail)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ErrorMessagesResource.EMAIL_CADASTRADO));
        }

        if (isThereUserWithCpf)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("cpf", ErrorMessagesResource.CPF_CADASTRADO));
        }

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
