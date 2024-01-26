using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Exceptions.ExceptionsBase
{
    public class InvalidLogInException : MeuLivroDeReceitasException
    {
        public InvalidLogInException() : base(ResourceErrorMessage.INVALID_LOGIN)
        {
            
        }
    }
}
