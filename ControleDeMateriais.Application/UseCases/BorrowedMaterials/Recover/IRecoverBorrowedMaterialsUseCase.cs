namespace ControleDeMateriais.Application.UseCases.BorrowedMaterials.Recover;
public interface IRecoverBorrowedMaterialsUseCase
{
    Task<List<string>> Execute(List<string> codeBar);
}
