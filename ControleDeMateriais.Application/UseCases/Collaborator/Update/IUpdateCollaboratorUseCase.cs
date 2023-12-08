using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Update;
public interface IUpdateCollaboratorUseCase
{
    Task<ResponseCollaboratorJson> Execute(string enrollment, RequestUpdateCollaboratorJson request);
}
