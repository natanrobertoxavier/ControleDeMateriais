using ControleDeMateriais.Domain.Entities;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository;
public class ConnectDataBase
{
    private const string user = "users";
    private const string recoveryCode = "recoverycodes";
    public static IMongoCollection<User> GetUserAccess()
    {
        var connection = Environment.GetEnvironmentVariable("ConnectionString");
        var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        var mongoClient = new MongoClient(connection);
        var mongoDataBase = mongoClient.GetDatabase(databaseName);

        IMongoCollection<User> collection = mongoDataBase.GetCollection<User>(user);

        return collection;
    }

    public static IMongoCollection<RecoveryCode> GetRecoveryCodeAccess()
    {
        var connection = Environment.GetEnvironmentVariable("ConnectionString");
        var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        var mongoClient = new MongoClient(connection);
        var mongoDataBase = mongoClient.GetDatabase(databaseName);

        IMongoCollection<RecoveryCode> collection = mongoDataBase.GetCollection<RecoveryCode>(recoveryCode);

        return collection;
    }
}
