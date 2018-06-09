using Livraria.Api.ObjectModel.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public string SessionId { get; set; }
        public List<LivroPedido> Itens { get; set; } 
        public decimal ValorTotal { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusPedido Status { get; set; }

    }
}
