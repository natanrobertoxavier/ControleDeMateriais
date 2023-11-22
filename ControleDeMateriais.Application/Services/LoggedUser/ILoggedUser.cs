using ControleDeMateriais.Domain.Entities;

namespace ControleDeMateriais.Application.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> RecoveryUser();
}
