namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Recover;
public interface IRecoverBorrowedMaterialUseCase
{
    Task<List<string>> Execute(List<string> codeBar);
}
