using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Loan.Recover;
public interface IRecoverMaterialForCollaboratorUseCase
{
    Task<List<ResponseMaterialForCollaboratorJson>> Execute();
    Task<List<ResponseMaterialForCollaboratorJson>> Execute(DateTime? initial, DateTime? final);
    Task<List<ResponseMaterialForCollaboratorJson>> Execute(bool status);
    Task<List<ResponseMaterialForCollaboratorJson>> Execute(string enrollment);
    Task<List<ResponseMaterialForCollaboratorJson>> Execute(string enrollment, bool status);
}
