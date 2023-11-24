using AutoMapper;
using ControleDeMateriais.Application.UseCases.Material.Register;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.Material.Update;
public class UpdateMaterialUseCase : IUpdateMaterialUseCase
{
    private readonly IMapper _mapper;
    private readonly IMaterialWriteOnlyRepository _repositoryMaterialWriteOnly;
    private readonly IMaterialReadOnlyRepository _repositoryMaterialReadOnly;

    public UpdateMaterialUseCase(
        IMapper mapper,
        IMaterialWriteOnlyRepository repositoryMaterialWriteOnly)
    {
        _mapper = mapper;
        _repositoryMaterialWriteOnly = repositoryMaterialWriteOnly;
    }
    public Task<ResponseMaterialJson> Execute(RequestUpdateMaterialJson request)
    {
        ValidateData(request);
        throw new NotImplementedException();
    }

    private async Task ValidateData(RequestUpdateMaterialJson request)
    {
        var barCodeBD = await _repositoryMaterialReadOnly.RecoverByBarCode(request.BarCode);

        if (barCodeBD.BarCode != request.BarCode) 
            throw new ControleDeMateriaisException(ErrorMessagesResource.ALTERACAO_CODIGO_BARRAS_NAO_PERMITIDA);


        var validator = new MaterialUpdateValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
