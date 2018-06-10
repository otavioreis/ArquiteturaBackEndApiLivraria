using Livraria.Api.ObjectModel;
using Livraria.Api.ObjectModel.Swagger.Examples;
using Livraria.Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace Livraria.Api.Controllers.v1.prv
{
    [Route("v1/private/[controller]")]
    public partial class LivrosController : Controller
    {
        private readonly ILivrariaRepository _livrariaRespository;

        public LivrosController(ILivrariaRepository livrariaRepository)
        {
            _livrariaRespository = livrariaRepository;
        }

        /// <summary>
        /// Insere mais um livro na livraria
        /// </summary>
        /// <param name="livroLivraria">Json que deverá ser informado no corpo da requisição</param>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [SwaggerRequestExample(typeof(LivroLivraria), typeof(LivroLivrariaExample))]
        [SwaggerResponseExample(200, typeof(ListaLivroLivrariaExample))]
        public async Task<IActionResult> Post([FromBody] LivroLivraria livroLivraria)
        {
            if(livroLivraria != null)
            {
                var livrosLivraria = await _livrariaRespository.InsertLivroAsync(livroLivraria);

                return Ok(livrosLivraria);
            }
            else
            {
                return BadRequest("Json de entrada não definido no corpo da requisição");
            }
        }
    }
}
