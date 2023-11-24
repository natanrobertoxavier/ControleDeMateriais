using AutoMapper;
using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Application.UseCases.Material.Update;
using ControleDeMateriais.Application.UseCases.User.Register;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation.Results;
using System.Runtime.CompilerServices;

namespace ControleDeMateriais.Application.UseCases.Material.Register;
public class RegisterMaterialUseCase : IRegisterMaterialUseCase
{
    private readonly IMapper _mapper;
    private readonly IMaterialWriteOnlyRepository _repositoryMaterialWriteOnly;
    private readonly IMaterialReadOnlyRepository _repositoryMaterialReadOnly;

    public RegisterMaterialUseCase(
        IMapper mapper, 
        IMaterialWriteOnlyRepository repositoryMaterialWriteOnly,
        IMaterialReadOnlyRepository repositoryMaterialReadOnly)
    {
        _mapper = mapper;
        _repositoryMaterialWriteOnly = repositoryMaterialWriteOnly;
        _repositoryMaterialReadOnly = repositoryMaterialReadOnly;
    }

    public async Task<ResponseMaterialJson> Execute(RequestRegisterMaterialJson request)
    {
        await ValidateData(request);

        var material = _mapper.Map<Domain.Entities.Material>(request);

        await _repositoryMaterialWriteOnly.Register(material);

        return _mapper.Map<ResponseMaterialJson>(material);
    }

    private async Task ValidateData(RequestRegisterMaterialJson request)
    {
        var validator = new MaterialValidator();
        var result = validator.Validate(request);

        var barCodeBD = await _repositoryMaterialReadOnly.RecoverByBarCode(request.BarCode);

        if (barCodeBD is not null)
            result.Errors.Add(new ValidationFailure("BarCode", ErrorMessagesResource.CODIGO_BARRAS_CADASTRADO));

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
