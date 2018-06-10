using Livraria.Api.ObjectModel.ValueObjects;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.ObjectModel.Swagger.Examples
{
    public class PedidoExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Pedido
            {
                Id = Guid.NewGuid(),
                SessionId = "1234567",
                Status = StatusPedido.EmAberto,
                Itens = new List<LivroPedido>()
                {
                    new LivroPedido()
                    {
                        Id = new Guid("3982ecc0-5e96-4aec-8f61-5712f0d090d6"),
                        Autor = "Otavio Augusto Reis",
                        Isbn = "isbn-otavio",
                        Nome = "O livro de como programar",
                        ValorIndividual = 11.1m,
                        Quantidade = 2,
                        ValorTotal = 22.2m
                    },
                    new LivroPedido()
                    {
                        Id = new Guid("afe2fd0e-c54d-4b01-aa7f-0b363e8a8d23"),
                        Autor = "Autor Um",
                        Isbn = "isbn-um",
                        Nome = "O livro um",
                        ValorIndividual = 22.2m,
                        ValorTotal = 22.2m,
                        Quantidade = 1
                    }
                },
                ValorTotal = 44.4m
            };
        }
    }
}
