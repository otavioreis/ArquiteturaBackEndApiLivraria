using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel.ValueObjects
{
    public enum StatusPedido
    {
        EmAberto = 0,
        EmProcessamento = 1,
        Processado = 2,
        PreparandoParaEnvio = 3,
        PedidoComTransportadora = 4,
        Recebido = 5
    }
}
