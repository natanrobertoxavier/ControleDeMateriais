using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Material.Update;
public interface IUpdateMaterialUseCase
{
    Task<ResponseMaterialJson> Execute(string id, RequestUpdateMaterialJson request);
}
