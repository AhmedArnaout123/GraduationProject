using GP.ECommerce1.Infrastructure.DataSeeder;
using GP.ECommerce1.Infrastructure.DataSeeder.Seeders;
using GP.ECommerce1.Infrastructure.MongoDb;
using GP.ECommerce1.Infrastructure.Sql;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbType = Environment.GetEnvironmentVariable("DB_TYPE");

if(dbType == "MongoDB")
    builder.Services.AddMongoDbInfrastructure();
else if (dbType == "SQL")
    builder.Services.AddSqlInfrastructure();
else 
    throw new Exception("Couldn't Identify the Value of the DB_TYPE Environment Variable.");


// using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
// {
//     var mediator = serviceProvider.GetRequiredService<IMediator>();
//     var seedingManager = new DataSeedingManager(mediator);
//     var os = new ShoppingCartsAndWishListsSeeder(mediator);
//     await os.Seed();
// }

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();