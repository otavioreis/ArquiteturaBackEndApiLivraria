using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel
{
    public class UsuarioLogin
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime AuthTime { get; set; }
    }
}
