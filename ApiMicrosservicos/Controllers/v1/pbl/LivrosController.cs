using Livraria.Api.Extensions;
using Livraria.Api.ObjectModel;
using Livraria.Api.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var livros = await _livrariaRespository.GetLivrosAsync();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var livro = await _livrariaRespository.GetLivrosPorIdAsync(id);
            return Ok(livro);
        }

        [Route("{id}/comentario")]
        [HttpPost]
        public async Task<IActionResult> PostComentario(int id)
        {
            return await Task.Run(() => Ok($"comentário recebido - id livro {id.ToString()}"));
        }


        [Route("autor/{valor}")]
        [HttpGet]
        public async Task<IActionResult> GetLivrosPorAutor(string valor)
        {
            var livros = await _livrariaRespository.GetLivrosPorFiltroAsync(l => l.Autor.ToLower().Contains(valor.ToLower()));
            return Ok(livros);
        }

        [Route("isbn/{valor}")]
        [HttpGet]
        public async Task<IActionResult> GetLivrosPorIsbn(string valor)
        {
            var livros = await _livrariaRespository.GetLivrosPorFiltroAsync(l => l.Isbn.ToLower().Contains(valor.ToLower()));
            return Ok(livros);
        }

        [Route("nome/{valor}")]
        [HttpGet]
        public async Task<IActionResult> GetLivrosPorNome(string valor)
        {
            var livros = await _livrariaRespository.GetLivrosPorFiltroAsync(l => l.Nome.ToLower().Contains(valor.ToLower()));
            return Ok(livros);
        }

        [Route("carrinho/{sessionId}")]
        [HttpGet]
        public IActionResult GetItensCarrinho(string sessionId)
        {
            var carrinho = HttpContext.Session.GetObject<List<ItemCarrinho>>(sessionId);

            if (carrinho == null)
                return Ok();

            return Ok(carrinho);
        }


        [Route("carrinho/{sessionId}")]
        [HttpPost]
        public IActionResult PostNovoItemCarrinho([FromBody]ItemCarrinho itemLivraria, string sessionId)
        {
            if (itemLivraria == null)
                return BadRequest("Verifique o json informado no corpo do request.");

            try
            {

                var carrinho = HttpContext.Session.GetObject<List<ItemCarrinho>>(sessionId);

                if (carrinho == null)
                    carrinho = new List<ItemCarrinho>();

                var itemCarrinho = carrinho.Find(r => r.Id == itemLivraria.Id);

                if (itemCarrinho != null)
                {
                    itemCarrinho.Quantidade += itemLivraria.Quantidade;
                }
                else
                {
                    carrinho.Add(itemLivraria);
                }

                HttpContext.Session.SetObject(sessionId, carrinho);



                return Ok(carrinho);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
