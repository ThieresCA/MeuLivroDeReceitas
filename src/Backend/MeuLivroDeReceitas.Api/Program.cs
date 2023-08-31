using FluentMigrator.Runner;
using MeuLivroDeReceitas.Infrastructure.Data;
using MeuLivroDeReceitas.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigureServices(builder.Services, builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrationDB();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
{
    var connectionString = Configuration.GetConnectionString("MeuLivroDeReceitasDb");

    services.AddDbContext<MeuLivroDeReceitasContext>(options =>
                options.UseSqlServer(connectionString, builder =>
                    builder.MigrationsAssembly("MeuLivroDeReceitas.Infrastructure")));

    services.AddFluentMigratorCore().ConfigureRunner(configure =>
        configure.AddSqlServer()
        .WithGlobalConnectionString(connectionString).ScanIn(typeof(MigrationExtension).Assembly).For.All()
        );
}
