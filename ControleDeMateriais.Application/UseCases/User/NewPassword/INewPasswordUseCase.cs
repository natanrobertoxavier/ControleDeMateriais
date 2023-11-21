using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.User.NewPassword;
public interface INewPasswordUseCase
{
    Task<ResponseNewPasswordJson> Execute(RequestNewPasswordJson request);
}
