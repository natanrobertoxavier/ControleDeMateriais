using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Collaborator.ConfirmPassword;
public interface IConfirmPasswordUseCase
{
    Task<ResponseCollaboratorJson> Execute(string enrollment, string password);
}
