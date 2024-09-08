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

2. **Docker compose**:
    - Execute o projeto pelo comando do Docker Compose ou no Visual Studio na mesma opção.
    ```bash
    docker-compose build
    docker-compose up

  ![image](https://github.com/user-attachments/assets/841f865d-7d4d-4e8e-a620-930689d8e518)

3. **Banco de Dados**:
   - A instancia do MSSQL Server já foi gerada pelo docker compose.
   - Usuário "sa", senha "Senha@123".
   - As tabelas **Users**, **Products**, **Order** e **OrderItems** foram criadas e previamente populadas via script na inicialização da aplicação.

4. **Acessar a Documentação da API**:
   - Acesse o Swagger na URL: http://localhost:{porta}/swagger para visualizar e interagir com a documentação da API.
   
5. **Autenticação**:
    - Usando o Postman ou direto na API, realize uma chamada ao endpoint **/api/user/auth**, enviado no corpo da mensagem os parametros **"login": "dgduarte"** e **"password": "Senha@123"**, como nos exemplos abaixo.
      - ***Postman***
      ![image](https://github.com/user-attachments/assets/2555cbac-e192-472e-981b-8c72c89c0880)

       - ***Swagger**
      ![image](https://github.com/user-attachments/assets/adb0f125-6122-419f-b5d4-7a5fdc6fce8d)
   
    - Cópie o conteúdo de Token do response.
      ![image](https://github.com/user-attachments/assets/74afd33c-6623-4966-aa2c-02ff1fbb2d8d)

    - Adicione aos parametros Authorization dos enpoints no Postman, com o Auth Type Bearer Token, no campo Token o valor copiado e clique no botão SEND
      ![image](https://github.com/user-attachments/assets/58e090f0-7e8c-4841-8134-997922a3d9de)

      - ou
    - No Swagger da Api clique no botão **Authorize**, no campo value adicione a palavra "Bearer", de um espaço, cole o valor copiado de Token e clique em Authorize.
      ![image](https://github.com/user-attachments/assets/e43cbc33-45c4-41c2-b5bb-45bdb5769ae5)

5. **Teste à vontade**:
    - E vamos conversar. 

## Observações
1. Tanto o código quanto as requisições e respostas da API estão em inglês, só o título e esse README em português (rs).
2. Os testes unitários e o algoritimo de criptografia da senha do usuário foram gerados pela IA Generativa GPT-4o da Sai-Library da Stefanini, algumas correções foram necessárias, mas ajudou bastante.
3. Usei o Dapper, o JWT Bearer e os padrões de Response usando Data e ReportErrors, por considerar esses padrões mais utilizados ultimamente. Com isso acabei não criando o banco com o Entity Framework Core, o que acabou gerando a necessidade de executar um script para o banco de dados na inicialização da API. 

## Extras

1. **Collection Postman DesafioApi**: [Collection](https://github.com/DimasTorres/Desafio/blob/master/Desafio%20.Net.postman_collection.json)
2. **Repositorio da imagem da aplicação e do banco de dados no Docker Hub**:
    ```bash
      docker pull dimastorres/desafiopresentationapi
      docker pull dimastorres/mssql-server-2022
