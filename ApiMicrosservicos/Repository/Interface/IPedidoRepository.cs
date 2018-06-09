using Livraria.Api.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.Repository.Interface
{
    public interface IPedidoRepository
    {
        Task<bool> InsertPedidoAsync(Pedido pedido);
        Task<Pedido> GetPedidoPorIdAsync(Guid id);
        Task<Pedido> GetPedidoPorSessionId(string sessionId);
    }
}
