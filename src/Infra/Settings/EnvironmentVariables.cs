using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infra.ORM.Settings;

public static class EnvironmentVariables
{
    public static string GetDatabaseConnectionString()
    {
        var databaseConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

        if (string.IsNullOrEmpty(databaseConnectionString))
            databaseConnectionString = GetConfigurations().GetConnectionString("DefaultConnection");

        return databaseConnectionString ?? "";
    }

    public static string GetMongoConnectionString()
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");

        if (string.IsNullOrEmpty(connectionString))
            connectionString = GetConfigurations().GetSection("MongoSettings:ConnectionString").Value;

        return connectionString ?? "";
    }

    public static string GetMongoDatabaseName()
    {
        var databaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME");

        if (string.IsNullOrEmpty(databaseName))
            databaseName = GetConfigurations().GetSection("MongoSettings:DatabaseName").Value;

        return databaseName ?? "";
    }

    private static IConfigurationRoot GetConfigurations()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    }
}