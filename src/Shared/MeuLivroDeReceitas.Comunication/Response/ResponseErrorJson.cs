using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Comunication.Response
{
    public class ResponseErrorJson
    {
        public List<string> ErrorsMessages { get; set; }

        public ResponseErrorJson(string mensagem)
        {
           ErrorsMessages = new List<string>
           {
               mensagem
           };
        }

        public ResponseErrorJson(List<string> mensagens)
        {
            ErrorsMessages = mensagens;
        }
    }
}
