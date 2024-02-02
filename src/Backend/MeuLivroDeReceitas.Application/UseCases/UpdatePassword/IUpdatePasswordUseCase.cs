using MeuLivroDeReceitas.Comunication.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Application.UseCases.UpdatePassword
{
    public interface IUpdatePasswordUseCase
    {
        Task Execute(UpdatePasswordRequestJson request);
    }
}
