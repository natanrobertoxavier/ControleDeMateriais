using ControleDeMateriais.Communication.Requests;

namespace ControleDeMateriais.Application.UseCases.Loan.Confirm;
public interface IConfirmSelectedMaterialUseCase
{
    Task Execute(RequestConfirmSelectedMaterialJson request);
}
