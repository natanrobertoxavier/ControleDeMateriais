using ControleDeMateriais.Communication.Responses;
using MongoDB.Bson;

namespace ControleDeMateriais.Application.UseCases.User.Recover;
public interface IRecoverUserUseCase
{
    Task<ResponseUserJson> Execute(string id);
}
