using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Recover;
public interface IRecoverCollaboratorUseCase
{
    Task<List<ResponseCollaboratorJson>> Execute();
    Task<ResponseCollaboratorJson> Execute(string enrollment);
}
