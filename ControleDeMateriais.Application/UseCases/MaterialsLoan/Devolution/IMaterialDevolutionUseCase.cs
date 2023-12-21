using ControleDeMateriais.Communication.Requests;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Devolution;
public interface IMaterialDevolutionUseCase
{
    Task Execute(RequestMaterialDevolutionJson request);
}
