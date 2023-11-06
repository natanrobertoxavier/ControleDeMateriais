using MongoDB.Driver;

namespace ControleDeMateriais.Infrastructure.Migrations;
public static class Database
{
    public static void CreateSchema(string connectionString, string schemaName)
    {
        var myConection = new MongoClient(connectionString);
        IMongoDatabase dataBase = myConection.GetDatabase(schemaName);

        var collections = dataBase.ListCollectionNames().ToList();

        if (!collections.Any())
        {
        }
    }
}
