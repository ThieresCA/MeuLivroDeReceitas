using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Comunication.Response
{
    public class ErrorResponseJson
    {
        public List<string> ErrorsMessages { get; set; }

        public ErrorResponseJson(string mensagem)
        {
           ErrorsMessages = new List<string>
           {
               mensagem
           };
        }

        public ErrorResponseJson(List<string> mensagens)
        {
            ErrorsMessages = mensagens;
        }
    }
}
