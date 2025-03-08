using Ecommerce.Application;
using Ecommerce.Application.Consumers;
using Ecommerce.Domain.Repositories;
using Ecommerce.Infra.ORM;
using Ecommerce.Infra.ORM.Repositories;
using Ecommerce.Infra.ORM.Repositories.NoSQL;
using Ecommerce.Infra.ORM.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DefaultContext>(options =>
{
    options.UseSqlServer(EnvironmentVariables.GetDatabaseConnectionString());
});

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderCreatedConsumer>();

    config.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(EnvironmentVariables.GetMongoConnectionString()));

builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    return client.GetDatabase(EnvironmentVariables.GetMongoDatabaseName());
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderMongoRepository, OrderMongoRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(ApplicationLayer).Assembly,
        typeof(Program).Assembly
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var bus = scope.ServiceProvider.GetRequiredService<IBusControl>();
    await bus.StartAsync();
}

app.Run();