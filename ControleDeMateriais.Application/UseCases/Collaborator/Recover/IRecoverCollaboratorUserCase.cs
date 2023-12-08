using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Recover;
public interface IRecoverCollaboratorUserCase
{
    Task<List<ResponseCollaboratorJson>> Execute();
    Task<ResponseCollaboratorJson> Execute(string enrollment);
}
