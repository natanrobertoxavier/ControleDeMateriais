using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.MaterialsLoan.Recover;
public interface IRecoverMaterialForCollaboratorUseCase
{
    Task<List<ResponseMaterialForCollaboratorJson>> Execute();
    Task<List<ResponseMaterialForCollaboratorJson>> Execute(bool status);
    Task<List<ResponseMaterialForCollaboratorJson>> Execute(string barCode);
    Task<List<ResponseMaterialForCollaboratorJson>> Execute(string enrollment, bool status);
    Task<List<string>> Execute(List<string> codeBar);
}
