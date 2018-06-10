using Livraria.Api.Extensions;
using Livraria.Api.ObjectModel;
using Livraria.Api.ObjectModel.Swagger.Examples;
using Livraria.Api.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Controllers.v1.pbl
{
    [Route("v1/public/[controller]")]
    public partial class LivrosController : Controller
    {
        private readonly ILivrariaRepository _livrariaRespository;

        public LivrosController(ILivrariaRepository livrariaRepository)
        {
            _livrariaRespository = livrariaRepository;
        }

        /// <summary>
        /// Lista todos os livros da livraria
        /// </summary>
        [SwaggerResponseExample(200, typeof(ListaLivroLivrariaExample))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var livros = await _livrariaRespository.GetLivrosAsync();
            return Ok(livros);
        }

        /// <summary>
        /// Busca um livro da livraria pelo seu Guid
        /// </summary>
        /// <param name="id">Guid do Livro</param>
        /// <response code="400">Bad Request</response>
        [SwaggerResponseExample(200, typeof(LivroLivrariaExample))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var livro = await _livrariaRespository.GetLivrosPorIdAsync(id);
            return Ok(livro);
        }

        /// <summary>
        /// Adiciona um comentário a um livro
        /// </summary>
        /// <param name="id">Guid do Livro</param>
        /// <param name="valor">comentário que deverá ser enviado</param>
        /// <response code="400">Bad Request</response>
        /// <response code="200">String informando o comentário recebido</response>
        [Route("{id}/comentario/{valor}")]
        [HttpPost]
        public async Task<IActionResult> PostComentario(Guid id, string valor)
        {
            var livro = await _livrariaRespository.GetLivrosPorIdAsync(id);

            if (livro == null)
                return BadRequest("Livro não encontrado");


            return await Task.Run(() => Ok($"comentário recebido {valor} - id livro {id.ToString()}"));
        }

        /// <summary>
        /// Busca um livro da livraria pelo seu Autor
        /// </summary>
        /// <param name="valor">String com o nome do autor</param>
        /// <response code="400">Bad Request</response>
        [SwaggerResponseExample(200, typeof(ListaLivroLivrariaExample))]
        [Route("autor/{valor}")]
        [HttpGet]
        public async Task<IActionResult> GetLivrosPorAutor(string valor)
        {
            var livros = await _livrariaRespository.GetLivrosPorFiltroAsync(l => l.Autor.ToLower().Contains(valor.ToLower()));
            return Ok(livros);
        }

        /// <summary>
        /// Busca um livro da livraria pelo seu Isbn
        /// </summary>
        /// <param name="valor">String com o nome do Isbn do livro</param>
        /// <response code="400">Bad Request</response>
        [SwaggerResponseExample(200, typeof(ListaLivroLivrariaExample))]
        [Route("isbn/{valor}")]
        [HttpGet]
        public async Task<IActionResult> GetLivrosPorIsbn(string valor)
        {
            var livros = await _livrariaRespository.GetLivrosPorFiltroAsync(l => l.Isbn.ToLower().Contains(valor.ToLower()));
            return Ok(livros);
        }

        /// <summary>
        /// Busca um livro da livraria pelo nome do Livro
        /// </summary>
        /// <param name="valor">String com o nome do livro</param>
        /// <response code="400">Bad Request</response>
        [SwaggerResponseExample(200, typeof(ListaLivroLivrariaExample))]
        [Route("nome/{valor}")]
        [HttpGet]
        public async Task<IActionResult> GetLivrosPorNome(string valor)
        {
            var livros = await _livrariaRespository.GetLivrosPorFiltroAsync(l => l.Nome.ToLower().Contains(valor.ToLower()));
            return Ok(livros);
        }

        /// <summary>
        /// Busca os itens de um carrinho dada uma sessionId
        /// </summary>
        /// <param name="sessionId">String a sessionId</param>
        /// <response code="400">Bad Request</response>
        [SwaggerResponseExample(200, typeof(ListaItemCarrinhoExample))]
        [Route("carrinho/{sessionId}")]
        [HttpGet]
        public IActionResult GetItensCarrinho(string sessionId)
        {
            var carrinho = HttpContext.Session.GetObject<List<ItemCarrinho>>(sessionId);

            if (carrinho == null)
                return Ok();

            return Ok(carrinho);
        }

        /// <summary>
        /// Insere um novo item a um carrinho dada uma sessionId
        /// </summary>
        /// <param name="itemCarrinho">Json com o model ItemCarrinho que deverá ser inserido</param>
        /// <param name="sessionId">String a sessionId</param>
        /// <response code="400">Bad Request</response>
        [SwaggerResponseExample(200, typeof(ListaItemCarrinhoExample))]
        [Route("carrinho/{sessionId}")]
        [HttpPost]
        public IActionResult PostNovoItemCarrinho([FromBody]ItemCarrinho itemCarrinho, string sessionId)
        {
            if (itemCarrinho == null)
                return BadRequest("Verifique o json informado no corpo do request.");

            try
            {

                var carrinho = HttpContext.Session.GetObject<List<ItemCarrinho>>(sessionId);

                if (carrinho == null)
                    carrinho = new List<ItemCarrinho>();

                var itemCarrinhoAux = carrinho.Find(r => r.Id == itemCarrinho.Id);

                if (itemCarrinhoAux != null)
                {
                    itemCarrinhoAux.Quantidade += itemCarrinho.Quantidade;
                }
                else
                {
                    carrinho.Add(itemCarrinho);
                }

                HttpContext.Session.SetObject(sessionId, carrinho);



                return Ok(carrinho);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove os itens de um carrinho dada uma sessionId
        /// </summary>
        /// <param name="id">Guid do livro a ser removido do carrinho</param>
        /// <param name="sessionId">String a sessionId</param>
        /// <response code="400">Bad Request</response>
        [SwaggerResponseExample(200, typeof(ListaItemCarrinhoExample))]
        [Route("{id}/carrinho/{sessionId}")]
        [HttpDelete]
        public IActionResult DeleteItemCarrinho(Guid id, string sessionId)
        {
            var carrinho = HttpContext.Session.GetObject<List<ItemCarrinho>>(sessionId);

            if (carrinho == null)
                return BadRequest("Carrinho inexistente.");

            var itemCarrinho = carrinho.Find(r => r.Id == id);

            if (itemCarrinho != null)
                carrinho.Remove(itemCarrinho);
            else
                return BadRequest("Item não encontrado.");

            HttpContext.Session.SetObject(sessionId, carrinho);

            return Ok(carrinho);
        }


    }
}
