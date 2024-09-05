
using Microsoft.EntityFrameworkCore;
using WebApplicationObras.Context;


var builder = WebApplication.CreateBuilder(args);

// Configura el puerto usando la variable de entorno `PORT`
var port = Environment.GetEnvironmentVariable("PORT") ?? "80";

Console.WriteLine($"PORT: {port}");

builder.WebHost.UseUrls($"http://*:{port}");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? builder.Configuration.GetConnectionString("PostgresConecction");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

Console.WriteLine(connectionString);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
