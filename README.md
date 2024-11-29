# BookStore API
  A BookStore API é uma API RESTful desenvolvida em .NET 8.0 utilizando arquitetura MVC, padrões de design CQRS e Repository, e princípios SOLID. Esta API gerencia livros, autores, categorias, vendas, usuários e inventário em um sistema de livraria.

## Tecnologias Utilizadas
  .NET 8.0
  Entity Framework Core (SQL Server)
  JWT Authentication
  AutoMapper
  Swagger
  CQRS
  Repository Pattern
  Swashbuckle para documentação Swagger

## Pré-requisitos
  Antes de começar, você precisa ter os seguintes requisitos instalados em sua máquina:

  .NET 8.0 SDK
  SQL Server ou SQL Server Express para banco de dados (se estiver utilizando o SQL Server localmente)
  Postman ou ferramenta similar para testar as requisições API (opcional, mas recomendado)
  Swagger está configurado para facilitar a documentação da API e testes diretamente na interface.
  Configuração

1. Clone o Repositório
Clone este repositório em sua máquina:

git clone https://github.com/seuusuario/api-bookStore.git

2. Restaure os Pacotes NuGet
Após clonar o repositório, restaure os pacotes NuGet necessários para o projeto:

cd api-bookStore
dotnet restore

3. Configure a Conexão com o Banco de Dados
No arquivo appsettings.Development.json, configure a string de conexão com seu banco de dados:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=DIGITE AQUI O NOME DO SEU SERVIDOR;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}

4. Execute as Migrations
Para criar o banco de dados e as tabelas, execute o comando de migração:

dotnet ef migrations add FirstMigration
dotnet ef database update

5. Execute a API
Agora, você pode rodar a API localmente com o seguinte comando:

dotnet run

A API estará disponível no endereço http://localhost:5268/api ou https://localhost:7223/api

Endpoints da API
A API oferece endpoints para CRUD de Livros, Autores, Categorias, Vendas, Usuários e Inventário. Além disso, a API possui autenticação via JWT.

Você pode acessar a documentação da API utilizando o Swagger UI em:

http://localhost:5268/api/swagger

Exemplo de Endpoints

Autenticação
POST /api/auth/singIn - Realiza o login e retorna um token JWT.

Book
GET /api/books/{categoryaName} - Retorna todos os livros. É possível filtrar por categoria, basta enviar o nome como parâmetro. 
GET /api/book/{id} - Retorna um livro específico pelo ID.
GET /api/bookByTitle/{title} - Retorna um livro específico pelo título.
POST /api/books - Cria um novo livro.
PUT /api/books/{id} - Atualiza um livro existente.
DELETE /api/books/{id} - Deleta um livro pelo ID.

Author
GET /api/authors - Retorna todos os autores.
GET /api/author/{id} - Retorna um autor específico.
POST /api/author - Cria um novo autor.
PUT /api/author/{id} - Atualiza um autor existente.
DELETE /api/author/{id} - Deleta um autor pelo ID.

Category
GET /api/category - Retorna todos as categorias.
GET /api/category/{id} - Retorna uma categoria específica.
POST /api/category - Cria ua nova categoria.
PUT /api/category/{id} - Atualiza uma categoria existente.
DELETE /api/category/{id} - Deleta uma categoria pelo ID.

User
GET /api/users - Retorna todos os usuários.
POST /api/user - Cria um novo usuário.
PUT /api/user/{id} - Atualiza um usuário existente.
DELETE /api/user/{id} - Deleta um usuário pelo ID.

Sale
POST /api/sale - Registra uma venda.
GET /api/sales - Retorna todos as vendas.

Autenticação JWT
Os endpoints que requerem autenticação utilizam JWT (JSON Web Token). Para obter o token JWT, faça login com as credenciais do usuário no endpoint /api/auth/login.

Exemplo de requisição para login:

POST /api/auth/login
{
  "username": "usuario@example.com",
  "password": "senha123"
}

A resposta será um token JWT que você deve usar no cabeçalho Authorization para fazer requisições autenticadas:

Authorization: Bearer {seu-token-jwt}

Estrutura de Pastas
A estrutura de pastas segue o padrão MVC com CQRS e Repository. Abaixo está uma visão geral das pastas principais:

├───App
│   ├───Config         # Arquivos de configuração (ex: AutoMapper, JWT, Swagger)
│   ├───DataBase       # Definições do contexto e tabelas do banco de dados
│   ├───Enums          # Arquivos de Enum
│   ├───Exceptions     # Exceções customizadas
│   ├───Middlewares    # Middlewares de processamento
│   ├───Modules        # Módulos do sistema (Autenticação, Autores, Livros, etc.)
│   └───Services       # Serviços de terceiros (AutoMapper, JWT, Swagger)

Se quiser saber um pouco mais sobre as arquiterua, organização de pastas, arquivos e design, navegue até a pasta documentation, lá existe um arquivo com mais detalhes.

Atenciosamente,
Rafael Magno



