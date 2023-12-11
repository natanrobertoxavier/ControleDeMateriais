using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Selection;
public interface IMaterialSelectionUseCase
{
    Task Execute(RequestMaterialSelectionJson request);
}
