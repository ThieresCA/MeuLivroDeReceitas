using MeuLivroDeReceitas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Application.Services.LogedInUser
{
    public interface ILoggedInUser
    {
        Task<User> RetrieveUser();
    }
}
