using ControleDeMateriais.Communication.Requests;

namespace ControleDeMateriais.Application.UseCases.Loan.Devolution;
public interface IMaterialDevolutionUseCase
{
    Task Execute(RequestMaterialDevolutionJson request);
}
