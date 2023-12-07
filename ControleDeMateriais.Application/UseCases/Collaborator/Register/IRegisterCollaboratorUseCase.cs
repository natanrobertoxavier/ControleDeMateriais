using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Collaborator.Register;
public interface IRegisterCollaboratorUseCase
{
    Task<ResponseCollaboratorCreatedJson> Execute(RequestCollaboratorJson request);
}
