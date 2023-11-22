using AutoMapper;
using ControleDeMateriais.Application.Services.LoggedUser;
using ControleDeMateriais.Application.UseCases.User.Register;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Material;
using ControleDeMateriais.Exceptions.ExceptionBase;

namespace ControleDeMateriais.Application.UseCases.Material.Register;
public class RegisterMaterialUseCase : IRegisterMaterialUseCase
{
    private IMapper _mapper;
    private IMaterialWriteOnlyRepository _repositoryMaterialWriteOnly;

    public RegisterMaterialUseCase(
        IMapper mapper, 
        IMaterialWriteOnlyRepository repositoryMaterialWriteOnly)
    {
        _mapper = mapper;
        _repositoryMaterialWriteOnly = repositoryMaterialWriteOnly;
    }

    public async Task<ResponseRegisterMaterialJson> Execute(RequestRegisterMaterialJson request)
    {
        ValidateData(request);

        var material = _mapper.Map<Domain.Entities.Material>(request);

        await _repositoryMaterialWriteOnly.Register(material);

        return _mapper.Map<ResponseRegisterMaterialJson>(material);
    }

    private static void ValidateData(RequestRegisterMaterialJson request)
    {
        var validator = new MaterialValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ExceptionValidationErrors(messageError);
        }
    }
}
