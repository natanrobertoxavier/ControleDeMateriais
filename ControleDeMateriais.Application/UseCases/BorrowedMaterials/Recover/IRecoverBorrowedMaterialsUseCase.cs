using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.BorrowedMaterials.Recover;
public interface IRecoverBorrowedMaterialsUseCase
{
    Task<List<ResponseBorrowedMaterialJson>> Execute();
    Task<List<string>> Execute(List<string> codeBar);
}
