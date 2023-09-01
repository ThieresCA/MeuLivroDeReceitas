using MeuLivroDeReceitas.Domain.Entities;
using MeuLivroDeReceitas.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Infrastructure.Data.Repository
{
    public class UsuarioRepository : IUsuarioWriteOnlyRepository, IUsuarioReadOnlyRepository
    {
        private readonly MeuLivroDeReceitasContext _context;
        public UsuarioRepository(MeuLivroDeReceitasContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUser(Usuario usuario)
        {
            try
            {
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> EmailAlreadyExists(string email)
        {
            return await _context.Usuarios.AnyAsync(e => e.Email.Equals(email));
        }
    }
}
