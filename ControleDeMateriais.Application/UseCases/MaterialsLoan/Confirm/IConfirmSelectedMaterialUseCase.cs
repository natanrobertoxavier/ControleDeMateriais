using ControleDeMateriais.Communication.Requests;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Confirm;
public interface IConfirmSelectedMaterialUseCase
{
    Task Execute(RequestConfirmSelectedMaterialJson request);
}
