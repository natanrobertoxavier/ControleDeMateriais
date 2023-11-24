using AutoMapper;
using ControleDeMateriais.Application.UseCases.Material.Register;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;

namespace ControleDeMateriais.Application.UseCases.Material.Update;
public class UpdateMaterialUseCase : IUpdateMaterialUseCase
{
    private readonly IMapper _mapper;
    private readonly IMaterialWriteOnlyRepository _repositoryMaterialWriteOnly;
    private readonly IMaterialReadOnlyRepository _repositoryMaterialReadOnly;

    public UpdateMaterialUseCase(
        IMapper mapper,
        IMaterialWriteOnlyRepository repositoryMaterialWriteOnly,
        IMaterialReadOnlyRepository repositoryMaterialReadOnly)
    {
        _mapper = mapper;
        _repositoryMaterialWriteOnly = repositoryMaterialWriteOnly;
        _repositoryMaterialReadOnly = repositoryMaterialReadOnly;
    }
    public async Task<ResponseMaterialJson> Execute(string id, RequestUpdateMaterialJson request)
    {
        await ValidateData(id, request);

        var entity = _mapper.Map<Domain.Entities.Material>(request);

        var result = await _repositoryMaterialWriteOnly.Update(id, entity);

        return _mapper.Map<ResponseMaterialJson>(result);
    }

    private async Task ValidateData(string id, RequestUpdateMaterialJson request)
    {
        var barCodeBD = await _repositoryMaterialReadOnly.RecoverById(id);

        var validator = new MaterialUpdateValidator();
        var result = validator.Validate(request);

        if (barCodeBD is null)
        {
            result.Errors.Add(new ValidationFailure("BarCode", ErrorMessagesResource.NENHUM_MATERIAL_LOCALIZADO));
            ThrowValidationErrors(result);
        }
        
        if (barCodeBD.BarCode != request.BarCode)
        {
            result.Errors.Add(new ValidationFailure("BarCode", ErrorMessagesResource.ALTERACAO_CODIGO_BARRAS_NAO_PERMITIDA));
            ThrowValidationErrors(result);
        }
    }

    private void ThrowValidationErrors(ValidationResult result)
    {
        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
