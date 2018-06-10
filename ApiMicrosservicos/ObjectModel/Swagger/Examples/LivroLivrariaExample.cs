using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel.Swagger.Examples
{
    public class LivroLivrariaExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new LivroLivraria
            {
                Autor = "Autor Teste",
                Isbn = "isbn-teste",
                Nome = "Nome Livro Teste",
                Valor = 100m
            };
        }
    }
}
