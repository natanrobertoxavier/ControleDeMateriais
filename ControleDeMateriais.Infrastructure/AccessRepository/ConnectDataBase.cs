using ControleDeMateriais.Domain.Entities;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository;
public class ConnectDataBase
{
    private const string user = "users";
    public static IMongoCollection<User> GetUserAccess()
    {
        var connection = Environment.GetEnvironmentVariable("ConnectionString");
        var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        var mongoClient = new MongoClient(connection);
        var mongoDataBase = mongoClient.GetDatabase(databaseName);

        IMongoCollection<User> collection = mongoDataBase.GetCollection<User>(user);

        return collection;
    }
}
