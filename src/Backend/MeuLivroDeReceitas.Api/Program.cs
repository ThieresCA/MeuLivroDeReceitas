using FluentMigrator.Runner;
using MeuLivroDeReceitas.Api.Filters;
using MeuLivroDeReceitas.Application.Services.AutoMapper;
using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Application.UseCases.User.SignUp;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Infrastructure.Data;
using MeuLivroDeReceitas.Infrastructure.Data.Repository;
using MeuLivroDeReceitas.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigureServices(builder.Services, builder.Configuration);
builder.Services.AddSwaggerGen();

//adicionando um filtro para que cada vez q um erro estoure a classe de exceptionFilters será chamada
builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilters)));

//configurando o perfil do AutoMapper
builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperConfiguration());
}).CreateMapper());


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


    services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    services.AddScoped<ISignUpUseCase, SignUpUseCase>();

    var sectionTokenKey = Configuration.GetRequiredSection("Configuration:TokenKey");
    var sectionLifeTime = Configuration.GetRequiredSection("Configuration:LifeTimeToken");

    services.AddScoped(option => new TokenController(int.Parse(sectionLifeTime.Value), sectionTokenKey.Value));
}
