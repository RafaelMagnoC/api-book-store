Documentação da API - BookStore
Visão Geral
A API BookStore é construída utilizando o framework .NET 8, adotando a arquitetura MVC (Model-View-Controller) e os padrões de design CQRS (Command Query Responsibility Segregation) e Repository. Ela serve para gerenciar dados relacionados a livros, autores, vendas, categorias e usuários, oferecendo funcionalidades de autenticação, CRUD (Create, Read, Update, Delete), e outros serviços relacionados ao gerenciamento de uma livraria.

Principais Pacotes Utilizados
A API utiliza diversos pacotes para fornecer funcionalidades chave, como autenticação JWT, ORM (Entity Framework Core) para interação com o banco de dados e AutoMapper para mapeamento de objetos. Veja os principais pacotes:

Microsoft.AspNetCore.OpenApi: Para documentar a API com OpenAPI (Swagger).
Swashbuckle.AspNetCore: Para a geração da interface Swagger para testes e documentação da API.
Microsoft.EntityFrameworkCore: Para acesso a dados utilizando ORM com Entity Framework Core.
AutoMapper: Para mapear objetos de maneira eficiente entre entidades e DTOs.
Microsoft.IdentityModel.Tokens e System.IdentityModel.Tokens.Jwt: Para autenticação via tokens JWT.
Microsoft.AspNetCore.Authentication.JwtBearer: Para integração de autenticação JWT com a API.
Princípios de Arquitetura
A API segue os princípios SOLID de design de software, que garantem um código limpo e de fácil manutenção. As principais diretrizes são:

S: Single Responsibility Principle (Princípio da Responsabilidade Única)
O: Open/Closed Principle (Princípio Aberto/Fechado)
L: Liskov Substitution Principle (Princípio da Substituição de Liskov)
I: Interface Segregation Principle (Princípio da Segregação da Interface)
D: Dependency Inversion Principle (Princípio da Inversão de Dependência)
Arquitetura de Pastas e Arquivos
A estrutura do projeto é organizada para separar responsabilidades e facilitar a navegação no código. A seguir, detalho a função de cada pasta e arquivo no projeto.

1. Pasta App
A pasta principal do projeto contém subpastas que agrupam componentes com responsabilidades distintas. Ela inclui as seguintes subpastas:

Config: Contém classes de configuração que ajudam a manter o Program.cs mais limpo e organizado.

AutoMapperConfig.cs: Configuração do AutoMapper.
CorsConfig.cs: Configuração de CORS.
DataBaseConfig.cs: Configuração do contexto de banco de dados (EF Core).
JwtConfig.cs: Configuração de autenticação JWT.
SwaggerConfig.cs: Configuração do Swagger para documentação da API.
TransientConfig.cs: Configuração de injeção de dependência.
DataBase: Contém o arquivo que define o contexto do banco de dados e as entidades.

BookStoreContext.cs: Contexto do Entity Framework, que representa o banco de dados.
Enums: Contém arquivos que definem enums para uso em toda a aplicação.

Exemplo: CountriesEnum.cs, RolesEnum.cs, SaleStatusEnum.cs.
Exceptions: Armazena exceções customizadas para casos de erro específicos.

Exemplo: BadRequest.cs, NotFound.cs, UniqueKeyDuplicate.cs.
Middleware: Contém classes que definem middleware customizados.

CustomException.cs: Middleware para capturar exceções globalmente.
Modules: Agrupa os módulos que representam as funcionalidades principais da API, como autenticação, gerenciamento de livros, usuários, vendas, etc.

Cada módulo segue a estrutura:
Controller: Responsável pelas requisições HTTP.
DTO: Data Transfer Object, para otimização de dados transmitidos.
Entity: Representação das entidades do banco de dados.
Interface: Definição de contrato para os repositórios.
Repository: Implementação dos repositórios para acesso aos dados.
ViewModel: Modelos usados na interação com o usuário, como criação e atualização de registros.

Exemplos de módulos:

Auth: Responsável pela autenticação.
Author: Gerenciamento de autores.
Book: Gerenciamento de livros.
Category: Gerenciamento de categorias.
Sale: Gerenciamento de vendas.
User: Gerenciamento de usuários.
Services: Agrupa serviços de terceiros, como AutoMapper, JWT e Swagger.

AutoMapper: Configura o mapeamento de objetos.
Jwt: Implementa a geração e validação de tokens JWT.
Swagger: Configura o Swagger para gerar a documentação da API.

2. Pasta Migrations
Contém arquivos relacionados ao gerenciamento de migrações do Entity Framework Core, que são responsáveis por criar e atualizar o esquema do banco de dados.

20241129193900_First.cs: Exemplo de uma migração.
BookStoreContextModelSnapshot.cs: Snapshot do estado atual do modelo do banco de dados.

3. Pasta bin
Contém os arquivos gerados durante a compilação do projeto, como assemblies, arquivos de configuração e dependências do runtime.

4. Pasta obj
Armazena arquivos temporários gerados pela compilação, como cache de build e dependências compiladas.

5. Pasta Properties
Contém arquivos de configuração do projeto, como launchSettings.json, que define as configurações de execução e ambientes.

Map:

│   api-bookStore.csproj
│   api-bookStore.http
│   api-bookStore.sln
│   appsettings.Development.json
│   appsettings.json
│   Program.cs
│   
├───App
│   ├───Config
│   │       AutoMapperConfig.cs
│   │       CorsConfig.cs
│   │       DataBaseConfig.cs
│   │       JwtConfig.cs
│   │       SwaggerConfig.cs
│   │       TransientConfig.cs
│   │
│   ├───DataBase
│   │       BookStoreContext.cs
│   │
│   ├───Enums
│   │       CountriesEnum.cs
│   │       RolesEnum.cs
│   │       SaleStatusEnum.cs
│   │
│   ├───Exceptions
│   │       AvailableQuantity.cs
│   │       BadRequest.cs
│   │       CreateException.cs
│   │       InvalidCredential.cs
│   │       NotAuthenticated.cs
│   │       NotAuthorized.cs
│   │       NotFound.cs
│   │       RemoveException.cs
│   │       UniqueKeyDuplicate.cs
│   │       UpdateException.cs
│   │
│   ├───Middlewares
│   │       CustomException.cs
│   │
│   ├───Modules
│   │   ├───Auth
│   │   │   ├───Controller
│   │   │   │       AuthController.cs
│   │   │   │
│   │   │   ├───DTO
│   │   │   │       AuthDTO.cs
│   │   │   │
│   │   │   ├───Entitiy
│   │   │   │       AuthEntity.cs
│   │   │   │
│   │   │   ├───Interface
│   │   │   │       IAuthRepository.cs
│   │   │   │
│   │   │   ├───Repository
│   │   │   │       AuthRepository.cs
│   │   │   │
│   │   │   └───ViewModel
│   │   │           AuthViewModel.cs
│   │   │
│   │   ├───Author
│   │   │   ├───Controller
│   │   │   │       AuthorController.cs
│   │   │   │
│   │   │   ├───DTO
│   │   │   │       AuthorDTO.cs
│   │   │   │
│   │   │   ├───Entity
│   │   │   │       AuthorEntity.cs
│   │   │   │
│   │   │   ├───Interface
│   │   │   │       IAuthorRepository.cs
│   │   │   │
│   │   │   ├───Repository
│   │   │   │       AuthorRepository.cs
│   │   │   │
│   │   │   └───ViewModel
│   │   │           AuthorViewModelCreate.cs
│   │   │           AuthorViewModelUpdate.cs
│   │   │
│   │   ├───Book
│   │   │   ├───Controller
│   │   │   │       BookController.cs
│   │   │   │
│   │   │   ├───DTO
│   │   │   │       BookDTO.cs
│   │   │   │
│   │   │   ├───Entity
│   │   │   │       BookEntity.cs
│   │   │   │
│   │   │   ├───Interface
│   │   │   │       IBookRepository.cs
│   │   │   │
│   │   │   ├───Repository
│   │   │   │       BookRepository.cs
│   │   │   │
│   │   │   └───ViewModel
│   │   │           BookViewModelCreate.cs
│   │   │           BookViewModelUpdate.cs
│   │   │
│   │   ├───Category
│   │   │   ├───Controller
│   │   │   │       CategoryController.cs
│   │   │   │
│   │   │   ├───DTO
│   │   │   │       CategoryDTO.cs
│   │   │   │
│   │   │   ├───Entity
│   │   │   │       CategoryEntity.cs
│   │   │   │
│   │   │   ├───Interface
│   │   │   │       ICategoryRepository.cs
│   │   │   │
│   │   │   ├───Repository
│   │   │   │       CategoryRepository.cs
│   │   │   │
│   │   │   └───ViewModel
│   │   │           CategoryViewModelCreate.cs
│   │   │           CategoryViewModelUpdate.cs
│   │   │
│   │   ├───Inventory
│   │   │   ├───DTO
│   │   │   │       InventoryDTO.cs
│   │   │   │
│   │   │   ├───Entity
│   │   │   │       InventoryEntity.cs
│   │   │   │
│   │   │   ├───Interface
│   │   │   │       IInventoryRepository.cs
│   │   │   │
│   │   │   ├───Repository
│   │   │   │       InventoryRepository.cs
│   │   │   │
│   │   │   └───ViewModel
│   │   │           InventoryViewModelCreate.cs
│   │   │           InventoryViewModelUpdate.cs
│   │   │
│   │   ├───Sale
│   │   │   ├───Controller
│   │   │   │       SaleController.cs
│   │   │   │
│   │   │   ├───DTO
│   │   │   │       SaleDTO.cs
│   │   │   │
│   │   │   ├───Entity
│   │   │   │       SaleEntity.cs
│   │   │   │       SaleXBookEntity.cs
│   │   │   │
│   │   │   ├───Interface
│   │   │   │       ISaleRepository.cs
│   │   │   │
│   │   │   ├───Repository
│   │   │   │       SaleRepository.cs
│   │   │   │
│   │   │   └───ViewModel
│   │   │           SaleViewModelCreate.cs
│   │   │           SaleViewModelUpdate.cs
│   │   │
│   │   └───User
│   │       ├───Controller
│   │       │       UserController.cs
│   │       │
│   │       ├───DTO
│   │       │       UserDTO.cs
│   │       │
│   │       ├───Entity
│   │       │       UserEntity.cs
│   │       │
│   │       ├───Interface
│   │       │       IUserRepository.cs
│   │       │
│   │       ├───Repository
│   │       │       UserRepository.cs
│   │       │
│   │       ├───Service
│   │       │       PasswordServiceHash.cs
│   │       │
│   │       └───ViewModel
│   │               UserViewModelCreate.cs
│   │               UserViewModelUpdate.cs
│   │
│   └───Services
│       ├───AutoMapper
│       │       AutoMapperService.cs
│       │
│       ├───Jwt
│       │       JwtTokenService.cs
│       │
│       └───Swagger
│               SwaggerService.cs
│
├───bin
│   └───Debug
│       └───net8.0
│           │   api-bookStore.deps.json
│           │   api-bookStore.dll
│           │   api-bookStore.exe
│           │   api-bookStore.pdb
│           │   api-bookStore.runtimeconfig.json
│           │   api-bookStore.xml
│           │   appsettings.Development.json
│           │   appsettings.json
│           │   AutoMapper.dll
│           │   AutoMapper.Extensions.Microsoft.DependencyInjection.dll
│           │   Azure.Core.dll
│           │   Azure.Identity.dll
│           │   Humanizer.dll
│           │   Microsoft.AspNetCore.Authentication.JwtBearer.dll
│           │   Microsoft.AspNetCore.OpenApi.dll
│           │   Microsoft.Bcl.AsyncInterfaces.dll
│           │   Microsoft.Bcl.TimeProvider.dll
│           │   Microsoft.CodeAnalysis.CSharp.dll
│           │   Microsoft.CodeAnalysis.CSharp.Workspaces.dll
│           │   Microsoft.CodeAnalysis.dll
│           │   Microsoft.CodeAnalysis.Workspaces.dll
│           │   Microsoft.Data.SqlClient.dll
│           │   Microsoft.EntityFrameworkCore.Abstractions.dll
│           │   Microsoft.EntityFrameworkCore.Design.dll
│           │   Microsoft.EntityFrameworkCore.dll
│           │   Microsoft.EntityFrameworkCore.Relational.dll
│           │   Microsoft.EntityFrameworkCore.SqlServer.dll
│           │   Microsoft.Extensions.Caching.Memory.dll
│           │   Microsoft.Extensions.DependencyInjection.Abstractions.dll
│           │   Microsoft.Extensions.DependencyInjection.dll
│           │   Microsoft.Extensions.DependencyModel.dll
│           │   Microsoft.Extensions.Logging.Abstractions.dll
│           │   Microsoft.Extensions.Logging.dll
│           │   Microsoft.Extensions.Options.dll
│           │   Microsoft.Identity.Client.dll
│           │   Microsoft.Identity.Client.Extensions.Msal.dll
│           │   Microsoft.IdentityModel.Abstractions.dll
│           │   Microsoft.IdentityModel.JsonWebTokens.dll
│           │   Microsoft.IdentityModel.Logging.dll
│           │   Microsoft.IdentityModel.Protocols.dll
│           │   Microsoft.IdentityModel.Protocols.OpenIdConnect.dll
│           │   Microsoft.IdentityModel.Tokens.dll
│           │   Microsoft.OpenApi.dll
│           │   Microsoft.SqlServer.Server.dll
│           │   Microsoft.Win32.SystemEvents.dll
│           │   Mono.TextTemplating.dll
│           │   Swashbuckle.AspNetCore.Swagger.dll
│           │   Swashbuckle.AspNetCore.SwaggerGen.dll
│           │   Swashbuckle.AspNetCore.SwaggerUI.dll
│           │   System.ClientModel.dll
│           │   System.CodeDom.dll
│           │   System.Composition.AttributedModel.dll
│           │   System.Composition.Convention.dll
│           │   System.Composition.Hosting.dll
│           │   System.Composition.Runtime.dll
│           │   System.Composition.TypedParts.dll
│           │   System.Configuration.ConfigurationManager.dll
│           │   System.Drawing.Common.dll
│           │   System.IdentityModel.Tokens.Jwt.dll
│           │   System.Memory.Data.dll
│           │   System.Runtime.Caching.dll
│           │   System.Security.Cryptography.ProtectedData.dll
│           │   System.Security.Permissions.dll
│           │   System.Windows.Extensions.dll
│           │
│           ├───cs
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───de
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───es
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───fr
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───it
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───ja
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───ko
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───pl
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───pt-BR
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───ru
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───runtimes
│           │   ├───unix
│           │   │   └───lib
│           │   │       └───net6.0
│           │   │               Microsoft.Data.SqlClient.dll
│           │   │               System.Drawing.Common.dll
│           │   │
│           │   ├───win
│           │   │   └───lib
│           │   │       └───net6.0
│           │   │               Microsoft.Data.SqlClient.dll
│           │   │               Microsoft.Win32.SystemEvents.dll
│           │   │               System.Drawing.Common.dll
│           │   │               System.Runtime.Caching.dll
│           │   │               System.Security.Cryptography.ProtectedData.dll
│           │   │               System.Windows.Extensions.dll
│           │   │
│           │   ├───win-arm
│           │   │   └───native
│           │   │           Microsoft.Data.SqlClient.SNI.dll
│           │   │
│           │   ├───win-arm64
│           │   │   └───native
│           │   │           Microsoft.Data.SqlClient.SNI.dll
│           │   │
│           │   ├───win-x64
│           │   │   └───native
│           │   │           Microsoft.Data.SqlClient.SNI.dll
│           │   │
│           │   └───win-x86
│           │       └───native
│           │               Microsoft.Data.SqlClient.SNI.dll
│           │
│           ├───tr
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           ├───zh-Hans
│           │       Microsoft.CodeAnalysis.CSharp.resources.dll
│           │       Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│           │       Microsoft.CodeAnalysis.resources.dll
│           │       Microsoft.CodeAnalysis.Workspaces.resources.dll
│           │
│           └───zh-Hant
│                   Microsoft.CodeAnalysis.CSharp.resources.dll
│                   Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll
│                   Microsoft.CodeAnalysis.resources.dll
│                   Microsoft.CodeAnalysis.Workspaces.resources.dll
│
├───Migrations
│       20241129193900_First.cs
│       20241129193900_First.Designer.cs
│       BookStoreContextModelSnapshot.cs
│
├───obj
│   │   api-bookStore.csproj.EntityFrameworkCore.targets
│   │   api-bookStore.csproj.nuget.dgspec.json
│   │   api-bookStore.csproj.nuget.g.props
│   │   api-bookStore.csproj.nuget.g.targets
│   │   project.assets.json
│   │   project.nuget.cache
│   │
│   └───Debug
│       └───net8.0
│           │   .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│           │   api-book.C6A4215E.Up2Date
│           │   api-bookStore.AssemblyInfo.cs
│           │   api-bookStore.AssemblyInfoInputs.cache
│           │   api-bookStore.assets.cache
│           │   api-bookStore.csproj.AssemblyReference.cache
│           │   api-bookStore.csproj.CoreCompileInputs.cache
│           │   api-bookStore.csproj.FileListAbsolute.txt
│           │   api-bookStore.dll
│           │   api-bookStore.GeneratedMSBuildEditorConfig.editorconfig
│           │   api-bookStore.genruntimeconfig.cache
│           │   api-bookStore.GlobalUsings.g.cs
│           │   api-bookStore.MvcApplicationPartsAssemblyInfo.cache
│           │   api-bookStore.MvcApplicationPartsAssemblyInfo.cs
│           │   api-bookStore.pdb
│           │   api-bookStore.xml
│           │   apphost.exe
│           │   staticwebassets.build.json
│           │
│           ├───ref
│           │       api-bookStore.dll
│           │
│           ├───refint
│           │       api-bookStore.dll
│           │
│           └───staticwebassets
│                   msbuild.build.api-bookStore.props
│                   msbuild.buildMultiTargeting.api-bookStore.props
│                   msbuild.buildTransitive.api-bookStore.props
│
└───Properties
        launchSettings.json

fim 