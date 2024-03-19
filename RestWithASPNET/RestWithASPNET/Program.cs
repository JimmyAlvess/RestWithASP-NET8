using EvolveDb;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Repository;
using Serilog;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Business.Implementations;
using RestWithASPNET.Repository.Generic;
using RestWithASPNETErudio.Model.Context;

var builder = WebApplication.CreateBuilder(args);

// Configuração do logger Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Configuração do serviço do Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware de migração de banco de dados para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    MigrateDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Método para realizar migração de banco de dados com Evolve
void MigrateDatabase(string connection)
{
    try
    {
        var sqlConnection = new SqlConnection(connection);
     var evolve = new Evolve(sqlConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    }
}
