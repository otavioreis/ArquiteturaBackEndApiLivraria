using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel.Swagger.Examples
{
    public class ListaLivroLivrariaExample : IExamplesProvider
    {
        public object GetExamples()
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
                }
            };
        }
    }
}
