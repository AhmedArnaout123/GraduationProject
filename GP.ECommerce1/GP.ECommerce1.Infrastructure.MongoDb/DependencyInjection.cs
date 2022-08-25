using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace GP.ECommerce1.Infrastructure.MongoDb;

public static class DependencyInjection
{

    public static void AddMongoDbInfrastructure(this IServiceCollection services)
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        services.AddSingleton<IMongoClient>(mongoClient);
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}