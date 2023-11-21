using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.User.ForgotPassword;
public interface IForgotPasswordUseCase
{
    Task<ResponseForgotPasswordJson> Execute(RequestForgotPasswordJson request);
}
