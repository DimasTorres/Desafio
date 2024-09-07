# Desafio

Bem-vindo ao projeto **Desafio**, uma aplicação de exemplo desenvolvida para um desafio de desenvolvimento para candidatos da Stefanini Group. Esta aplicação foi construída utilizando a linguagem de programação C# com .NET Core 8. A aplicação é uma API que implementa um CRUD para gerenciamento de pedidos, utilizando um banco de dados SQL Server.

## Visão Geral

O projeto foi desenvolvido seguindo princípios de design de software como Domain-Driven Design (DDD), SOLID, Clean Code, e API Restful. Esses princípios foram aplicados para garantir um código de alta qualidade, fácil manutenção e extensibilidade.

## Funcionalidades

- **CRUD de Pedidos**: Criação, leitura, atualização e exclusão de pedidos.
- **Autenticação JWT**: Segurança via tokens JWT Bearer.
- **Documentação**: Documentação da API utilizando Swagger.

## Tecnologias Utilizadas

- **C# e .NET Core 8**: Plataforma de desenvolvimento utilizada para criar a API.
- **SQL Server**: Banco de dados utilizado para armazenar os dados da aplicação.
- **Swagger**: Ferramenta para documentação interativa da API, permitindo testes diretamente no navegador.
- **Docker**: Utilizado para containerização da aplicação, facilitando a implantação e escalabilidade.
- **Mapper (AutoMapper)**: Ferramenta utilizada para mapear objetos de uma classe para outra, simplificando a lógica de conversão de dados.
- **Dapper**: Micro ORM para acesso rápido e eficiente ao banco de dados, utilizado para executar consultas SQL diretamente.
- **Validators (FluentValidation)**: Biblioteca para validação de objetos, garantindo que os dados atendam aos requisitos antes de serem processados.
- **JWT Bearer**: Implementação de autenticação e autorização utilizando tokens JWT, garantindo segurança nas chamadas à API.

## Princípios de Design

- **Domain-Driven Design (DDD)**: Abordagem de design focada na modelagem do domínio do negócio, garantindo que a lógica de negócios seja central no desenvolvimento.
- **SOLID**: Conjunto de princípios de design orientado a objetos que visa criar sistemas mais compreensíveis, flexíveis e com menos dependências.
- **Clean Code**: Práticas de escrita de código limpo, legível e sustentável, facilitando a manutenção e evolução do projeto.
- **API Rest**: Estilo arquitetural para comunicação entre sistemas, utilizando HTTP e padrões de URL para interações com recursos.
- **Unit of Work**: conceito utilizado em sistemas de persistência de dados para gerenciar transações de forma eficiente e consistente.
  
## Testes

- **xUnit**: Framework de teste utilizado para executar testes unitários na aplicação, garantindo o funcionamento correto das funcionalidades.
- **NSubstitute**: Biblioteca de mocking utilizada em conjunto com xUnit para simular dependências e testar unidades de código isoladamente.

## Como Executar

1. **Clone o Repositório**:
   ```bash
   git clone https://github.com/DimasTorres/Desafio.git

## Extras

1. **Script do Banco de Dados DesafioDB**: https://github.com/DimasTorres/Desafio/blob/master/ScriptDB.sql
2. **Collection Postman DesafioApi**: https://github.com/DimasTorres/Desafio/blob/master/Desafio%20.Net.postman_collection.json
