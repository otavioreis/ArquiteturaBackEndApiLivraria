using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Api.ObjectModel;
using Livraria.Api.Repository.Interface;

namespace Livraria.Api.Repository
{
    public class LivrariaRepository : ILivrariaRepository
    {
        private readonly Lazy<List<LivroLivraria>> _livrosLivrariaMock;

        public LivrariaRepository()
        {
            _livrosLivrariaMock = new Lazy<List<LivroLivraria>>(PopularLivrosLivrariaMock());
        }

        public List<LivroLivraria> LivrosLivrariaMock
        {
            get { return _livrosLivrariaMock.Value; }
        }


        public async Task<List<LivroLivraria>> GetLivrosAsync()
        {
            //Mock
            return await Task.Run(() => LivrosLivrariaMock);
        }

        public async Task<List<LivroLivraria>> GetLivrosPorFiltroAsync(Func<LivroLivraria, bool> filter)
        {
            //Mock
            return await Task.Run(() => LivrosLivrariaMock.Where(filter).ToList());
        }

        public async Task<LivroLivraria> GetLivrosPorIdAsync(Guid id)
        {
            //Mock
            return await Task.Run(() => LivrosLivrariaMock.Where(l => l.Id == id).FirstOrDefault());
        }

        public async Task<List<LivroPedido>> GetLivrosPorItensCarrinhoAsync(List<ItemCarrinho> itensCarrinho)
        {
            var livrosPedido = new List<LivroPedido>();

            foreach(var item in itensCarrinho)
            {
                var livroLivraria = await this.GetLivrosPorIdAsync(item.Id);

                if(livroLivraria != null)
                {
                    livrosPedido.Add(
                        new LivroPedido
                        {
                            Id = item.Id,
                            Autor = livroLivraria.Autor,
                            Isbn = livroLivraria.Isbn,
                            Nome = livroLivraria.Nome,
                            ValorIndividual = livroLivraria.Valor,
                            Quantidade = item.Quantidade,
                            ValorTotal = livroLivraria.Valor * item.Quantidade
                        }
                    );

                }
            };

            return livrosPedido;
        }

        public async Task<List<LivroLivraria>> InsertLivroAsync(LivroLivraria livroLivraria)
        {
            LivrosLivrariaMock.Add(new LivroLivraria()
            {
                Id = Guid.NewGuid(),
                Autor = livroLivraria.Autor,
                Isbn = livroLivraria.Isbn,
                Nome = livroLivraria.Nome,
                Valor = livroLivraria.Valor
            });

            return await Task.Run(() => LivrosLivrariaMock);
        }

        private List<LivroLivraria> PopularLivrosLivrariaMock()
        {
            return new List<LivroLivraria>()
            {
                new LivroLivraria()
                {
                    Id = new Guid("3982ecc0-5e96-4aec-8f61-5712f0d090d6"),
                    Autor = "Otavio Augusto Reis",
                    Isbn = "isbn-otavio",
                    Nome = "O livro de como programar",
                    Valor = 11.1m
                },
                new LivroLivraria()
                {
                    Id = new Guid("afe2fd0e-c54d-4b01-aa7f-0b363e8a8d23"),
                    Autor = "Autor Um",
                    Isbn = "isbn-um",
                    Nome = "O livro um",
                    Valor = 22.2m
                },
                new LivroLivraria()
                {
                    Id = new Guid("b2f1fe9d-8a1c-4a12-a774-4277f5c7e5be"),
                    Autor = "Autor Dois",
                    Isbn = "isbn-dois",
                    Nome = "O livro dois",
                    Valor = 33.3m
                }
            };
        }
    }
}
