using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel
{
    public class Comentario
    {
        public Guid IdLivro { get; set; }
        public string Texto { get; set; }
    }
}
