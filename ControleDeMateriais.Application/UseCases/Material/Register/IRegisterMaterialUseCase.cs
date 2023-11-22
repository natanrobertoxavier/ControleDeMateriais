using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Material.Register;
public interface IRegisterMaterialUseCase
{
    Task<ResponseRegisterMaterialJson> Execute(RequestRegisterMaterialJson request);
}
