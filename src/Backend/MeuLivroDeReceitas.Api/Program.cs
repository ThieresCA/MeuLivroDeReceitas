using MeuLivroDeReceitas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
{

    services.AddDbContext<MeuLivroDeReceitasContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MeuLivroDeReceitasDb"), builder =>
                    builder.MigrationsAssembly("MeuLivroDeReceitas.Infrastructure")));
}
