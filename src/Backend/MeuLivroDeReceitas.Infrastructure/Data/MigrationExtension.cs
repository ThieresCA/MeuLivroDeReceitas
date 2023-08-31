﻿using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MeuLivroDeReceitas.Infrastructure.Data
{
    public static class MigrationExtension
    {
        public static void MigrationDB(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.ListMigrations();

            runner.MigrateUp();
        }
    }
}
