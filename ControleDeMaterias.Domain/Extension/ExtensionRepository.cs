using Microsoft.Extensions.Configuration;

namespace ControleDeMateriais.Domain.Extension;
public static class ExtensionRepository
{
    public static string GetConnectionString(this IConfiguration configurationManager)
    {
        string connectionString = configurationManager.GetConnectionString("Connection");

        return connectionString;
    }

    public static string GetDatabaseName(this IConfiguration configurationManager)
    {
        string dataBaseName = configurationManager.GetConnectionString("DataBaseName");

        return dataBaseName;
    }
}
