namespace ControleDeMateriais.Application.UseCases.Material.Delete;
public interface IDeleteMaterialUseCase
{
    Task Execute(string barCode);
}
