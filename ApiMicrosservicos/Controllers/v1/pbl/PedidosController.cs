using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Api.Extensions;
using Livraria.Api.ObjectModel;
using Livraria.Api.ObjectModel.ValueObjects;
using Livraria.Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Api.Controllers.v1.pbl
{
    [Route("v1/public/[controller]")]
    public class PedidosController : Controller
    {
        private readonly ILivrariaRepository _livrariaRespository;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosController(ILivrariaRepository livrariaRepository, IPedidoRepository pedidoRepository)
        {
            _livrariaRespository = livrariaRepository;
            _pedidoRepository = pedidoRepository;
        }

        [HttpPost("{sessionId}")]
        public async Task<IActionResult> CriarPedido(string sessionId)
        {
            var pedidoCriadoAnteriormente = await _pedidoRepository.GetPedidoPorSessionId(sessionId);

            if (pedidoCriadoAnteriormente != null)
                return BadRequest("Este pedido já havia sido criado anterirmente para esta sessionId");

            var carrinho = HttpContext.Session.GetObject<List<ItemCarrinho>>(sessionId);

            if (carrinho == null)
                return BadRequest("Carrinho não encontrado.");

            if (carrinho.Count < 1)
                return BadRequest("Carrinho não possui itens.");

            var livrosPedido = await _livrariaRespository.GetLivrosPorItensCarrinhoAsync(carrinho);

            var novoPedido = new Pedido
            {
                Id = Guid.NewGuid(),
                SessionId = sessionId,
                Itens = livrosPedido,
                ValorTotal = livrosPedido.Sum(l => l.ValorTotal),
                Status = StatusPedido.EmAberto
            };

            var sucessoInsertPedido = await _pedidoRepository.InsertPedidoAsync(novoPedido);

            if(sucessoInsertPedido)
                return Ok(novoPedido);

            return BadRequest("Erro ao criar novo pedido");
        }

        [Route("{idPedido}")]
        [HttpGet]
        public async Task<IActionResult> GetDadosPedido(Guid idPedido)
        {
            var pedido = await _pedidoRepository.GetPedidoPorIdAsync(idPedido);

            if (pedido != null)
                return Ok(pedido);

            return BadRequest("Pedido não encontrado");
        }
    }
}
