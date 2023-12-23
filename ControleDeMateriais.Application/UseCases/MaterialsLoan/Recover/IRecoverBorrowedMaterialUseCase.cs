using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Recover;
public interface IRecoverBorrowedMaterialUseCase
{
    Task<List<ResponseBorrowedMaterialJson>> Execute();
    Task<List<ResponseBorrowedMaterialJson>> Execute(bool status);
    Task<List<ResponseBorrowedMaterialJson>> Execute(string enrollment, bool status);
    Task<List<string>> Execute(List<string> codeBar);
}
