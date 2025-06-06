using System.Data;
using DbConnectivity.Interfaces;
using DbConnectivity.MySql;
using DbConnectivity.Oracle;
using DbConnectivity.PostgreSQL;
using DbConnectivity.SQLServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DbConnectivity.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDBInsfrastractionRegistrationServices(this IServiceCollection services, IConfiguration configurationManager)
        {
            string dbServer = configurationManager.GetSection("dbServer").Value;
            string? connectionString = configurationManager.GetConnectionString("grocerydb");
            switch (dbServer)
            {
                case "sqlserver":
                    services.AddScoped<IDbConnection>(provider => new DapperDBConnection(provider, new SqlServerConnection(connectionString)));
                    services.AddScoped<IDatabaseConnection>(provider => new SqlServerConnection(connectionString));
                    break;
                case "oracle":
                    services.AddScoped<IDbConnection>(provider => new DapperDBConnection(provider, new OracleDatabaseConnection(connectionString)));
                    services.AddScoped<IDatabaseConnection>(provider => new OracleDatabaseConnection(connectionString));
                    break;
                case "postgresql":
                    services.AddScoped<IDbConnection>(provider => new DapperDBConnection(provider, new PostgreSQLConnection(connectionString)));
                    services.AddScoped<IDatabaseConnection>(provider => new PostgreSQLConnection(connectionString));
                    break;
                case "mysql":
                    services.AddScoped<IDbConnection>(provider => new DapperDBConnection(provider, new MySqlDatabaseConnection(connectionString)));
                    services.AddScoped<IDatabaseConnection>(provider => new MySqlDatabaseConnection(connectionString));
                    break;
            }
            return services;
        }
    }
}
