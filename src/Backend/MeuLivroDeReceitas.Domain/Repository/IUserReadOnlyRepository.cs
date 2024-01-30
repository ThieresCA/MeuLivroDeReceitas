﻿using MeuLivroDeReceitas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Domain.Repository
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> EmailAlreadyExists(string email);

        Task<User> Login(string email);
    }
}
