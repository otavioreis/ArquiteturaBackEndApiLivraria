using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel
{
    public class LivroLivraria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Isbn { get; set; }
        public string Autor { get; set; }
        public decimal Valor { get; set; }
    }
}
