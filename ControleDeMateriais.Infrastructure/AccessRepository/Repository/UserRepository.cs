using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.User;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    public async Task Add(User user)
    {
        var collection = ConnectDataBase.GetUserAccess();
        await collection.InsertOneAsync(user);
    }

    public async Task UpdatePassword(User user)
    {
        var collection = ConnectDataBase.GetUserAccess();
        var filter = Builders<User>.Filter.Where(c => c.Id == user.Id);

        _ = await collection.ReplaceOneAsync(filter, user);
    }

    public async Task<bool> IsThereUserWithEmail(string email)
    {
        var collection = ConnectDataBase.GetUserAccess();
        var result = await collection.FindAsync(c => c.Email.Equals(email));

        if (await result.AnyAsync())
            return true;

        return false;
    }

    public async Task<bool> IsThereUserWithCpf(string cpf)
    {
        var collection = ConnectDataBase.GetUserAccess();
        var result = await collection.FindAsync(c => c.Cpf.Equals(cpf));

        if (await result.AnyAsync())
            return true;

        return false;
    }

    public async Task<User> RecoverEmailPassword(string email, string password)
    {
        var collection = ConnectDataBase.GetUserAccess();
        var filter = Builders<User>.Filter.Where(c => c.Email.Equals(email) && c.Password.Equals(password));

        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User> IsThereUserWithEmailReturnUser(string email)
    {
        var collection = ConnectDataBase.GetUserAccess();
        var filter = Builders<User>.Filter.Where(c => c.Email.Equals(email));

        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User> RecoverByEmail(string email)
    {
        var collection = ConnectDataBase.GetUserAccess();
        var filter = Builders<User>.Filter.Where(c => c.Email.Equals(email));

        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User> RecoverById(ObjectId id)
    {
        var collection = ConnectDataBase.GetUserAccess();
        var filter = Builders<User>.Filter.Where(c => c.Id.Equals(id));

        return await collection.Find(filter).FirstOrDefaultAsync();
    }
}
