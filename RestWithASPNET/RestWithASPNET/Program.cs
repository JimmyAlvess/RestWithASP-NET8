using EvolveDb;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Repository;
using Serilog;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Business.Implementations;
using RestWithASPNET.Repository.Generic;
using RestWithASPNETErudio.Model.Context;
using Microsoft.AspNetCore.Mvc.Formatters;
using RestWithASPNETErudio.Hypermedia.Enricher;
using RestWithASPNET.Hypermedia.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
var appName = "REST API's RESTfull from to Azure with ASP .NET Core 8 and Docker";
var appVersion = "v1";
var appDescription = "REST API RESTful developed from 0 to Azure with ASP.NET Core 8 and Docker";

// Configuração do logger Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

// Configuração do serviço do Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();

builder.Services.AddControllersWithViews(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
})
.AddXmlSerializerFormatters();

var filterOptions = new HyperMidiaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

//Version API
builder.Services.AddApiVersioning();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = appName,
            Version = appVersion,
            Description = appDescription,
            Contact = new OpenApiContact
            {
                Name = "Matheus Alves",
                Url = new Uri("https://github.com/JimmyAlvess")
            }
        }); ; 
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware de migração de banco de dados para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    MigrateDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));
}

app.UseHttpsRedirection();

app.UseCors();

app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        $"{appName} - {appVersion}");
});

var options = new RewriteOptions();
options.AddRedirect("^$", "swagger");
app.UseRewriter(options);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

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
