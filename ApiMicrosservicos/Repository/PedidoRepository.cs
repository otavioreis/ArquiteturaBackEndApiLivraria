using Livraria.Api.ObjectModel;
using Livraria.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private List<Pedido> _pedidosDbMock;

        public PedidoRepository()
        {
            _pedidosDbMock = new List<Pedido>();
        }

        public async Task<Pedido> GetPedidoPorIdAsync(Guid id)
        {
            //Mock
            return await Task.Run(() => _pedidosDbMock.Where(l => l.Id == id).FirstOrDefault());
        }

        public async Task<Pedido> GetPedidoPorSessionId(string sessionId)
        {
            //Mock
            return await Task.Run(() => _pedidosDbMock.Where(l => l.SessionId == sessionId).FirstOrDefault());
        }

        public async Task<bool> InsertPedidoAsync(Pedido pedido)
        {
            //Mock
            if (pedido == null)
                return await Task.Run(() => false);

            _pedidosDbMock.Add(pedido);

            return await Task.Run(() => true);
        }
    }
}
