using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.BorrowedMaterials.Recover;
public interface IRecoverBorrowedMaterialsUseCase
{
    Task<List<ResponseBorrowedMaterialJson>> Execute();
    Task<List<ResponseBorrowedMaterialJson>> Execute(bool status, bool received);
    Task<List<ResponseBorrowedMaterialJson>> Execute(string barCode);
    Task<List<string>> Execute(List<string> codeBar);
}
