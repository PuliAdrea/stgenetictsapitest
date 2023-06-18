using STGenetics_API.Context;
using STGenetics_API.Contracts;
using STGenetics_API.Repository;
using STGenetics_API.Services.Contracts;
using STGenetics_API.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("SqlConnection");

builder.Services.AddSingleton<DapperContext>(serviceProvider =>
{
    return new DapperContext(connectionString);
});


builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
