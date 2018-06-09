using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel
{
    public class LivroPedido
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Isbn { get; set; }
        public string Autor { get; set; }
        public decimal ValorIndividual { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }

    }
}
