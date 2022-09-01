using System.Data.SqlClient;
using System.Reflection;
using GP.Utilix;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GP.ECommerce1.Infrastructure.Sql;

public static class DependencyInjection
{
    public static void AddSqlInfrastructure(this IServiceCollection services)
    {
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        if (dbName == null)
            throw new Exception("Couldn't Identify the Value of DB_NAME Variable");
        
        var connectionString =
            $"Server=DESKTOP-9FJ5CB1\\SQLEXPRESS;Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true";
        var connection = new SqlConnection(connectionString);
        services.AddSingleton(connection);
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}