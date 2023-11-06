using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class UserRepository : IUserWriteOnlyRepository
{
    public async Task Add(User user)
    {
        var collection = ConnectDataBase.GetUserAccess();
        await collection.InsertOneAsync(user);
    }
}
