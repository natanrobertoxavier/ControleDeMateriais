using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Material.Recover;
public interface IRecoverMaterialUseCase
{
    Task<List<ResponseMaterialJson>> Execute();
    Task<List<ResponseMaterialJson>> Execute(int category);
    Task<ResponseMaterialJson> Execute(string codeBar);
}
