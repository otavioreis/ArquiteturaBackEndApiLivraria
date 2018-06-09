using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel
{
    public class ItemCarrinho
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}
