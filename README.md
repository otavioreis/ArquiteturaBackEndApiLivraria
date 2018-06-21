# ArquiteturaBackEndApiLivraria
Projeto criado como resulução do exercício 02 da aula 02 da disciplina Arquitetura de Backend e Microsserviços do curso de pós-graduação de Arquitetura de Softwares Distribuídos e, requisitado pelo professor Marco Mendes.

Este projeto atualmente está utilizando tudo em memória e session para armazenar as informações, uma vez que é apenas um projeto de teste.

O projeto foi desenvolvido utilizando c# juntamente com ASP.NET Core 2.0

## Grupo
**_MATEUS SORIANO_** <br />
**_OTÁVIO AUGUSTO DE QUEIROZ REIS_**

## Informações adicionais do projeto

**URL configurada para o projeto - Ambiente de Desenvolvimento**<br />
http://localhost:56223</br></br>
**URL configurada para o projeto - Ambiente de Produção**</br>
http://livrariaapibackend.azurewebsites.net

**URL da documentação utilizando o Swagger**<br />
http://localhost:56223/swagger
![alt text](https://i.snag.gy/B8A0xo.jpg)

**Exemplo de utilização do desenho da API**

*BUSCA UM LIVRO PELO ID*
```
GET /v1/public/livros/{id}
```

![alt text](https://i.snag.gy/CRktDS.jpg)

 
### Abaixo as requisições de teste no Postman

https://www.getpostman.com/collections/154d63a338d792d9d016

### DESENHO DA API:


**LIVROS**

**MÉTODOS PÚBLICOS**

*POSTA UM COMENTÁRIO*
```
POST	/v1/public/livros/{id}/comentario/{valor}
```

*BUSCA UM LIVRO PELO ID*
```
GET /v1/public/livros/{id}
```

Json Livro:
```json
{
    "id": "3982ecc0-5e96-4aec-8f61-5712f0d090d6",
    "nome": "O livro de como programar",
    "isbn": "isbn-otavio",
    "autor": "Otavio Augusto Reis",
    "valor": 11.1
}
```

*BUSCA TODOS OS LIVROS*
```
GET /v1/public/livros
```

*BUSCA UM LIVRO POR UMA PROPRIEDADE*
```
GET	/v1/public/livros/autor
GET	/v1/public/livros/isbn
GET	/v1/public/livros/nome
```
Json de saída dos métodos acima (Array de livros):
```json
[
    {
        "id": "3982ecc0-5e96-4aec-8f61-5712f0d090d6",
        "nome": "O livro de como programar",
        "isbn": "isbn-otavio",
        "autor": "Otavio Augusto Reis",
        "preco": 11.1
    },
    {
        "id": "afe2fd0e-c54d-4b01-aa7f-0b363e8a8d23",
        "nome": "O livro um",
        "isbn": "isbn-um",
        "autor": "Autor Um",
        "preco": 22.2
    }
]
```


**MÉTODO PRIVADO**

*INSERINDO UM NOVO LIVRO*
```
POST	/v1/private/livros
```
Json a ser enviado no corpo da requisição:
 ```json
{
    "nome": "novo livro 2",
    "isbn": "isbn-novo livro 2",
    "autor": "Autor novo Livro 2",
    "valor": 44
}
```


Após inserir um livro o retorno será um json com todos os livros existentes:
```json
[
    {
        "id": "3982ecc0-5e96-4aec-8f61-5712f0d090d6",
        "nome": "O livro de como programar",
        "isbn": "isbn-otavio",
        "autor": "Otavio Augusto Reis",
        "valor": 11.1
    },
    {
        "id": "afe2fd0e-c54d-4b01-aa7f-0b363e8a8d23",
        "nome": "O livro um",
        "isbn": "isbn-um",
        "autor": "Autor Um",
        "valor": 22.2
    },
    {
        "id": "b2f1fe9d-8a1c-4a12-a774-4277f5c7e5be",
        "nome": "O livro dois",
        "isbn": "isbn-dois",
        "autor": "Autor Dois",
        "valor": 33.3
    },
    {
        "id": "695e129b-b416-4e74-919c-4b13c6e3b659",
        "nome": "novo livro 2",
        "isbn": "isbn-novo livro 2",
        "autor": "Autor novo Livro 2",
        "valor": 44
    }
]
```



**CARRINHO DE COMPRAS**

**MÉTODOS PÚBLICOS**

*INSERE UM ITEM AO CARRINHO*
```
POST	/v1/public/livros/carrinho/{session_id}
```

Json que deverá ser passado no corpo da requisição
```json
{
	"id":"3982ecc0-5e96-4aec-8f61-5712f0d090d6",
	"quantidade": "2"
}
```


Json de resposta são todos os itens que já estão no carrinho:
```json
[
    {
        "id": "3982ecc0-5e96-4aec-8f61-5712f0d090d6",
        "quantidade": 6
    }
]
```
	
	

*RETORNA TODOS OS ITENS DE UM CARRINHO*
```
GET	/v1/public/livros/carrinho/{session_id}
```

Json de resposta com todos os itens do carrinho:
```json
[
    {
        "id": "3982ecc0-5e96-4aec-8f61-5712f0d090d6",
        "quantidade": 6
    }
]
```

*REMOVE UM LIVRO DO CARRINHO (TODAS AS UNIDADES)*
```
DELETE	/v1/public/livros/{id}/carrinho/{session_id}
```

Json de resposta com todos os itens do carrinho após a remoção:
```json
[
    {
        "id": "3982ecc0-5e96-4aec-8f61-5712f0d090d6",
        "quantidade": 2
    }
]
```

**PEDIDOS**

**MÉTODOS PÚBLICOS**

*CRIA UM PEDIDO NOVO*
```
POST	/v1/public/pedido/{session_id}
```
*RETORNA O PEDIDO COM TODOS SEUS ATRIBUTOS, INCLUSIVE O STATUS*
```
GET	/v1/public/pedido/{id_pedido}
```

Ambos os métodos retornam o Json com os dados do pedido:
```json
{
    "id": "7f9d6b8a-3c63-4d39-8ab9-9837de2fcd63",
    "sessionId": "111222",
    "itens": [
        {
            "id": "3982ecc0-5e96-4aec-8f61-5712f0d090d6",
            "nome": "O livro de como programar",
            "isbn": "isbn-otavio",
            "autor": "Otavio Augusto Reis",
            "valorIndividual": 11.1,
            "quantidade": 4,
            "valorTotal": 44.4
        }
    ],
    "valorTotal": 44.4,
    "status": "EmAberto"
}
```




**Códigos de Retorno da API***
```
Retorno: 200 - OK - Pode retornar uma string ou um json
Retorno: 400 - BadRequest - Mensagem informativa com o erro
Retorno: 404 - NotFound - Página não encontrada, verificar rota
Retorno: 415 - Unsuported Media Type - Verificar corpo da mensagem
```

### Justificativas das regras implementadas na API
1) Todos os recursos da API são baseados em substantivos. Exemplo: comentario, autor, carrinho, pedido, livros.
2) As coleções como Livros e comentários estão com seus nomes no plural.
3) Para um sistema onde o usuário pode pesquisar livros, fazer compra e olhar status do pedido essas entidades descritas no desenho da API suprem a necessidade do negócio.
4) Todos os nossos recursos são mais simples. Temos no máximo 4 partes na URI de delete para passar o id a ser deletado e o id do carrinho. Os demais têm 3 ou menos.
5) No caso de cadastro de livros, por exemplo, é possível informar um ou muitos livros no corpo da requisição.
6) Não está contemplado data e hora no desenho dessa API.
7) A documentação da API mostra os modelos JSON (esquema de dados, formatos suportados, etc.) e as respostas esperadas. Os nomes dos recursos também são de fácil entendimento.
8) Essa API será hospedada em um local com certificado e com SSL em sua URL.
9) Toda essa API está na versão 1 que é indicada com o início de sua formação: Exemplo: dominio/v1/conteudo
10) Para o recurso de listagem de livros por autor e nome do livro, que pode ter muitos dados, existem limites para paginação.
11) Os códigos de retorno do HTTP estão aplicados retornando 200 quando OK. Em caso de erro os retornos também serão adequados como 400 e etc.

