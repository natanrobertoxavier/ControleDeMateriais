using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Material.Recover;
public interface IRecoverMaterialUseCase
{
    Task<ResponseMaterialJson> Execute(string codeBar);
}
