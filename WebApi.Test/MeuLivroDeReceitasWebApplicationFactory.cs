using MeuLivroDeReceitas.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test
{

    //configuração para criar um banco de teste toda vez que for rodar os testes da WebApi
    public class MeuLivroDeReceitasWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var description = services.SingleOrDefault(d => d.ServiceType == typeof(MeuLivroDeReceitasContext));

                    if (description != null)
                    {
                        services.Remove(description);
                    }

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<MeuLivroDeReceitasContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    var serviceProvider = services.BuildServiceProvider();

                    using var scope = serviceProvider.CreateScope();

                    var scopedService = scope.ServiceProvider;

                    var database = scopedService.GetRequiredService<MeuLivroDeReceitasContext>();

                    database.Database.EnsureDeleted();
                });
        }
    }
}
