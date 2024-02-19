using MeuLivroDeReceitas.Domain.Entities;
using MeuLivroDeReceitas.Domain.Repository;
using Microsoft.EntityFrameworkCore;



namespace MeuLivroDeReceitas.Infrastructure.Data.Repository
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUpdateOnlyRepository
    {
        private readonly MeuLivroDeReceitasContext _context;
        public UserRepository(MeuLivroDeReceitasContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUser(User usuario)
        {
            try
            {
                await _context.Users.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User> FindByEmail(string email)
        {
            //vamos adicionar o no-tracking pq essa é uma query só de leitura, então para que seja mais rapido será
            //adicionado o no-tracking(recomendação da microsoft)
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Equals(email));
        }

        public async Task<bool> EmailAlreadyExists(string email)
        {
            return await _context.Users.AnyAsync(e => e.Email.Equals(email));
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
