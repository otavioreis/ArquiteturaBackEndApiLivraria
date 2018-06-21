using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel
{
    public class DadosPagamento
    {
        public string NumeroCartao { get; set; }
        public decimal Valor { get; set; }
    }
}
