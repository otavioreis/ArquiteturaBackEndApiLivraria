using Livraria.Api.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.Repository.Interface
{
    public interface ILivrariaRepository
    {
        Task<List<LivroLivraria>> GetLivrosAsync();
        Task<LivroLivraria> GetLivrosPorIdAsync(Guid id);
        Task<List<LivroLivraria>> GetLivrosPorFiltroAsync(Func<LivroLivraria, bool> filter);
        Task<List<LivroLivraria>> InsertLivroAsync(LivroLivraria livroLivraria);
        Task<List<LivroPedido>> GetLivrosPorItensCarrinhoAsync(List<ItemCarrinho> itensCarrinho);


    }
}
