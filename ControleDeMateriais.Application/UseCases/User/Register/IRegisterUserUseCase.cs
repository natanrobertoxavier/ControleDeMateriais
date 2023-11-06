using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseUserCreatedJson> Execute(RequestRegisterUserJson request);
}
