using AutoMapper;
using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Repositories.Material;

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
        throw new NotImplementedException();
    }
}
