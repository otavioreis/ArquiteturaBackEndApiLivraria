using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Livraria.Api.ApiClient;
using Livraria.Api.Extensions;
using Livraria.Api.ObjectModel;
using Livraria.Api.ObjectModel.Swagger.Examples;
using Livraria.Api.ObjectModel.ValueObjects;
using Livraria.Api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Examples;

namespace Livraria.Api.Controllers.v1.pbl
{
    [Authorize]
    [Route("v1/public/[controller]")]
    public class PedidosController : Controller
    {
        private readonly ILivrariaRepository _livrariaRespository;
        private readonly IPedidoRepository _pedidoRepository;
        private HttpClient _httpClient;

        public PedidosController(ILivrariaRepository livrariaRepository, IPedidoRepository pedidoRepository)
        {
            _livrariaRespository = livrariaRepository;
            _pedidoRepository = pedidoRepository;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Cria um novo pedido de acordo com os itens de um carrinho de uma sessão
        /// </summary>
        /// <param name="sessionId">String a sessionId</param>
        /// <param name="numeroCartao">String contendo o número do cartão - O cartão válido é 5510 4481 5196 2381</param>
        /// <response code="400">Bad Request</response>
        [SwaggerResponseExample(200, typeof(PedidoExample))]
        [HttpPost("{sessionId}/{numeroCartao}")]
        public async Task<IActionResult> CriarPedido(string sessionId, string numeroCartao)
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

            var apiHttpClient = new ApiHttpClient(null, "http://localhost:62000/", _httpClient);


            var dadosPagamento = new DadosPagamento()
            {
                NumeroCartao = numeroCartao,
                Valor = livrosPedido.Sum(l => l.ValorTotal)
            };

            var pagamentoValido = Convert.ToBoolean(await apiHttpClient.HttpClient.PostAsync("v1/private/pagamento", JsonConvert.SerializeObject(dadosPagamento)));

            if (pagamentoValido)
            {
                var novoPedido = new Pedido
                {
                    Id = Guid.NewGuid(),
                    SessionId = sessionId,
                    Itens = livrosPedido,
                    ValorTotal = livrosPedido.Sum(l => l.ValorTotal),
                    Status = StatusPedido.EmAberto
                };

                var sucessoInsertPedido = await _pedidoRepository.InsertPedidoAsync(novoPedido);

                if (sucessoInsertPedido)
                    return Ok(novoPedido); 
            }
            else
            {
                return BadRequest("Erro ao validar o número do cartão");
            }

            return BadRequest("Erro ao criar novo pedido");
        }

        /// <summary>
        /// Retorna os dados de um pedido de acordo com o seu id
        /// </summary>
        /// <param name="idPedido">Guid do id do Pedido</param>
        /// <response code="400">Bad Request</response>
        [SwaggerResponseExample(200, typeof(PedidoExample))]
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
