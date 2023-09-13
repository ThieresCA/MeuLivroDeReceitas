using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuLivroDeReceitas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.Data
{
    public class MeuLivroDeReceitasContext : DbContext
    {
        public MeuLivroDeReceitasContext(DbContextOptions<MeuLivroDeReceitasContext> options) : base(options)
        {
        }

        // aqui é feito a conexão da tabela de usuário com a classe
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuLivroDeReceitasContext).Assembly);
        }
    }
}
