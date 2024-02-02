using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Comunication.Request
{
    public class UpdatePasswordRequestJson
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
