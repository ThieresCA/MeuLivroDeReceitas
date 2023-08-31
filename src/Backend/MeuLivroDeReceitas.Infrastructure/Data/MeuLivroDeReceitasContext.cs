using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.Data
{
    public class MeuLivroDeReceitasContext : DbContext
    {
        public MeuLivroDeReceitasContext(DbContextOptions<MeuLivroDeReceitasContext> options) : base(options)
        {
        }
    }
}
