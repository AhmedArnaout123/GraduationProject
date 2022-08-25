using GP.ECommerce1.Infrastructure.DataSeeder;
using GP.ECommerce1.Infrastructure.Sql;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlInfrastructure();

// using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
// {
//     var mediator = serviceProvider.GetRequiredService<IMediator>();
//     var seedingManager = new DataSeedingManager(mediator);
//     await seedingManager.SeedOrders(1);
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