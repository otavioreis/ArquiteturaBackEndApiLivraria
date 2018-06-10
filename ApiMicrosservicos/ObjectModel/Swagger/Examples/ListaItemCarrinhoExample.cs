using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel.Swagger.Examples
{
    public class ListaItemCarrinhoExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new List<ItemCarrinho>()
            {
                new ItemCarrinho
                {
                    Id = new Guid("3982ecc0-5e96-4aec-8f61-5712f0d090d6"),
                    Quantidade = 2
                },
                new ItemCarrinho
                {
                    Id =  new Guid("afe2fd0e-c54d-4b01-aa7f-0b363e8a8d23"),
                    Quantidade = 1
                }
            };
        }
    }
}
