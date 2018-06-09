using Livraria.Api.ObjectModel;
using Livraria.Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JObject json)
        {
            if(json != null)
            {
                var livroLivraria = JsonConvert.DeserializeObject<LivroLivraria>(json.ToString());
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
