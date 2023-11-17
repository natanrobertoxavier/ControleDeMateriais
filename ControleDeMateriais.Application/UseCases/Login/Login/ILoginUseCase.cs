using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;

namespace ControleDeMateriais.Application.UseCases.Login.Login;
public interface ILoginUseCase
{
    Task<ResponseLoginJson> Execute(RequestLoginJson request);
}
