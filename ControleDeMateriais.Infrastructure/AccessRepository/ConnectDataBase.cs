using ControleDeMateriais.Domain.Entities;
using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.AccessRepository;
public class ConnectDataBase
{
    private const string user = "users";
    private const string recoveryCode = "recoverycodes";
    private const string materials = "materials";
    private const string materialsDeletionLog = "materialsDeletionLog";
    private const string collaborator = "collaborators";
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

    public static IMongoCollection<Material> GetMaterialAccess()
    {
        var connection = Environment.GetEnvironmentVariable("ConnectionString");
        var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        var mongoClient = new MongoClient(connection);
        var mongoDataBase = mongoClient.GetDatabase(databaseName);

        IMongoCollection<Material> collection = mongoDataBase.GetCollection<Material>(materials);

        return collection;
    }

    public static IMongoCollection<MaterialDeletionLog> GetMaterialDeletionLogAccess()
    {
        var connection = Environment.GetEnvironmentVariable("ConnectionString");
        var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        var mongoClient = new MongoClient(connection);
        var mongoDataBase = mongoClient.GetDatabase(databaseName);

        IMongoCollection<MaterialDeletionLog> collection = mongoDataBase.GetCollection<MaterialDeletionLog>(materialsDeletionLog);

        return collection;
    }

    public static IMongoCollection<Collaborator> GetCollaboratorAccess()
    {
        var connection = Environment.GetEnvironmentVariable("ConnectionString");
        var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        var mongoClient = new MongoClient(connection);
        var mongoDataBase = mongoClient.GetDatabase(databaseName);

        IMongoCollection<Collaborator> collection = mongoDataBase.GetCollection<Collaborator>(collaborator);

        return collection;
    }
}
