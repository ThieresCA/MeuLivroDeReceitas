using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Exceptions.ExceptionsBase
{
    public class ValidationErrorsExceptions : MeuLivroDeReceitasException
    {
        public List<string> ErrorsMessage { get; set; }

        public ValidationErrorsExceptions( List<string> errorsMessage) : base(string.Empty)
        {
            ErrorsMessage = errorsMessage;
        }
    }
}
