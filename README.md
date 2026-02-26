# prova-pratica

Desafio: Cadastro e Consulta de Produtos

Instruções para o PROJETO CatalogoProdutosAPI

OBSERVAÇÃO: Alguns passos não serão necessários, pois o projeto está feito:

            Inicialize o Docker Desktop, se já tem instalado a etapa 1 não é necessário
            
            Se já tem instalado dotnet-sdk-6.0.428-win-x64.exe .NET 6, então a etapa 2 não é necessário
            
            Na etapa 3, as extensões são necessárias se o mesmo não estão instalados no VSCode
            
            Crie a pasta projects em Documents e dentro de projects cole o projeto CatalogoProdutosAPI e CatalogoProdutosAPI.Tests é                   
            necessário desta forma para que funcione, a etapa 4 é necessária
            
            A etapa 5 não é necessária, pois o projeto está feito
            
            A etapa 6 é necessária
            
            A etapa 7 é necessária
            
            A etapa 8 é necessária
            
            A etapa 9 não é necessária, pois o arquivo já foi removido
            
            A etapa 10 é necessária
            
            A etapa 11 é necessária
            
            A etapa 12 não é necessária, pois já tem no projeto
            
            A etapa 13 não é necessária, pois as configurações já estão no arquivo do projeto
            
            A etapa 14 não é necessária, pois as configurações ja estão no arquivo do projeto
            
            A etapa 15 não é necessária, pois o código já está contido no projeto
            
            A etapa 16 é necessária
            
            A etapa 17 não é necessária, pois as configurações já está contido no projeto
            
            A etapa 18 não é necessária, pois as configurações já está contido no projeto
            
            A etapa 19 é necessária
            
            A etapa 20 é necessária
            
            A etapa 21 é necessária
            
            A etapa 22 é necessária
            
            A etapa 23 é necessária
            
            A etapa 24 não é necessária, pois a pasta Migrations já está contido no projeto
            
            A etapa 25 é necessária
            
            A etapa 26 é necessária

1 - Instalação do Docker Desktop Installer e inicialize o mesmo

2 - Instalação do dotnet-sdk-6.0.428-win-x64.exe

3 - No VSCode instalação das extensões C#, C# Dev Kit, IntelliCode For C# Dev Kit e C# Extentions for jchannon

4 - Abra o terminal do VSCode digite cd documents, depois digite mkdir projects e no final digite cd projects, diretório C:\Users\seu-usuario\documents\projects>

5 - No terminal VSCode neste diretório C:\Users\seu-usuario\documents\projects> digite dotnet new webapi -n CatalogoProdutosAPI

6 - Agora abra o projeto CatalogoProdutosAPI, no VSCode vá em File->Open Folder, selecione o projeto ao qual feito CatalogoProdutosAPI, você verá a estrutura de arquivos e pastas do projeto CatalogoProdutosAPI

7 - Agora no terminal do VSCode digite o comando dotnet run, ele compilara o projeto e criara a pasta bin:

    Visualização no terminal:
    C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI> dotnet run
      Compilando...
      info: Microsoft.Hosting.Lifetime[14]
            Now listening on: https://localhost:7050
      info: Microsoft.Hosting.Lifetime[14]
            Now listening on: http://localhost:5184
      info: Microsoft.Hosting.Lifetime[0]
            Application started. Press Ctrl+C to shut down.
      info: Microsoft.Hosting.Lifetime[0]
            Hosting environment: Development
      info: Microsoft.Hosting.Lifetime[0]
            Content root path: C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI\
            
8 - Copie a url http://localhost:5184 e cole na barra de pesquisa do google, logo após digite na url swagger e ficara assim http://localhost:5184/swagger e enter, no final resultara em uma url assim: http://localhost:5184/swagger/index.html, você vera a página do projeto

9 - Na pasta Controllers delete o arquivo desnecessário WeatherForecastController.cs

10 - Agora, crie o banco de dados no postgres, observação apenas o bando de dados, não crie nenhuma tabela

11 - Crie o arquivo na raiz do projeto File->New File o arquivo .env com as seguintes configurações, onde houver "configurar_aqui" é apenas modificar para a conexão do banco de dados:

      POSTGRES_USER=configurar_aqui
      POSTGRES_PASSWORD=configurar_aqui
      POSTGRES_DB=configurar_aqui

      PGADMIN_DEFAULT_EMAIL=pgadmin4@pgadmin.org
      PGADMIN_DEFAULT_PASSWORD=configurar_aqui

      ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=configurar_aqui;Username=configurar_aqui;Password=configurar_aqui
     
12 - Depois na raiz do projeto crie o arquivo em File->New File nome do arquivo docker-compose.yml

13 - Dentro deste arquivo docker-compose.yml digite essas configurações, as variáveis seguidas de $ serão reconhecidas, pois estão no arquivo .env:

version: '3.4'

services:

    postgresql_database:
        image: postgres:latest
        environment:
            - POSTGRES_USER=${POSTGRES_USER}
            - POSTGRES_PASSWORD=${POSTGRES_PASSWORD}
            - POSTGRES_DB=${POSTGRES_DB}
        ports:
            - "5432:5432"
        restart: always
        volumes: 
            - database-data:/var/lib/postgresql/data/

    pgadmin:
        image: dpage/pgadmin4
        environment:
            - PGADMIN_DEFAULT_EMAIL=${PGADMIN_DEFAULT_EMAIL}
            - PGADMIN_DEFAULT_PASSWORD=${PGADMIN_DEFAULT_PASSWORD}
        ports:
            - "5050:80"
        restart: always
        volumes:
            - pgadmin:/root/.pgadmin

    volumes:
        database-data:
        pgadmin:

14 - Agora no arquivo appsettings.json, digite:

     "ConnectionStrings": {
    	"DefaultConnection": ""
     },

   - A estrutura ficara assim no arquivo:
     
{

  "ConnectionStrings": {
      "DefaultConnection": ""
  },
  "Logging": {
      "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
      }
  },
  "AllowedHosts": "*"
  
}

15 - Na raiz do projeto no arquivo Program.cs, para carregar o arquivo .env, digite a linha de código DotNetEnv.Env.Load(); antes do builder, código var builder = WebApplication.CreateBuilder(args);

16 - Para que a linha de código DotNetEnv.Env.Load(); seja reconhecido no projeto, no terminal do VSCode digite C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI>dotnet add package DotNetEnv

17 - Carregando o arquivo .env, digite os seguites comandos em Program.cs para que no arquivo appsettings.json seja carregado e lido:
     
     // AddDbContext
     builder.Services.AddDbContext<DataContext>(options =>
         options.UseNpgsql(
             builder.Configuration.GetConnectionString("DefaultConnection")));

18 - E para evitar algum tipo de erro no carregamento da página, problemas de autenticação ou algo parecido utilizar a aplicação de cors depois da linha de código builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();, assim :

    builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

Em seguida digite a linha de código logo apos a linha de código app.MapControllers();, assim:

    app.MapControllers();

    app.UseCors("AllowAll");

Isso evita, algum tipo de erro na página

19 - Agora, feito as configurações necessárias, inicie o Docker Desktop antes e no terminal do VSCode pare os serviços do projeto digite Ctrl+C, agora digite o seguinte comando C:\Users\seu-usuario\Documents\projects2\CatalogoProdutosAPI>docker-compose up -d, este comando sobe o arquivo docker-compose.yml, criando os conteiners e grava as variáveis do arquivo .env

20 - Para a integração com o banco de dados digite o comando no terminal do VSCode C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI>dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 6.0.29

21 - Para instalar o dotnet-ef digite o seguinte comando no terminal do VSCode C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI>dotnet tool install --global dotnet-ef --version 6.*

22 - Para utilizar migração digite o seguinte comando no terminal do VSCode C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI>dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.29

23 - Para a instalação do EntityFramework, digite o seguinte comando no terminal do VSCode C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI>dotnet add package Microsoft.EntityFrameworkCore --version 6.0.29

24 - Agora para dar build e inicializar a migração, digite o seguinte comando no terminal do VSCode C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI>dotnet ef migrations add InitialMigration, em seguida ele cria a pasta Migations na estrutura do projeto

25 - E para criar a tabela Produtos, digite a seguinte linha de comando no terminal do VSCode C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI>dotnet ef database update

26 - Configurações concluída, digite o comando no terminal do VSCode C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI>dotnet run rode o projeto para testes

=====================================================================================

Instruções para o PROJETO CatalogoProdutosAPI.Tests

OBESERVAÇÕES: Alguns passos não serão necessários, pois o projeto está feito:

              A etapa 1 não é necessária, pois é apenas abrir o projeto no VSCode

              A etapa 2 não é necessária, pois o projeto já está feito

              A Etapa 3 é necessária

              A etapa 4 não é necessária, pois já foi feita a referenciação no projeto principal 

              A etapa 5 não é necessária, pois a referência já foi aplicada e já está contido no projeto

              A etapa 6 não é necessária, pois o código já está contido no projeto

              A etapa 7 não é necessária, pois o pacote já está contido no projeto

              A etapa 8 não é necessária, pois o pacote já está contido no projeto

              A etapa 9 não é necessária, pois é preciso ficar no projeto

              A etapa 10 não é necessária, pois o pacote já está contido no projeto

              A etapa 11 é apenas a confirmação de que o pacote ja está no projeto

              A etapa 12 é necessária

              A etapa 13 é apenas a confirmação da validação do projeto em relação a API principal

              Os pacotes estão neste arquivo CatalogoProdutosAPI.Tests.csproj              

1 - No terminal do VSCode digite cd Documents, depois cd projects Diretório final C:\Users\seu-usuario\Documents\projects>

2 - No terminal do VSCode digite o seguinte comando para criar o projeto de testes C:\Users\seu-usuario\Documents\projects>dotnet new xunit -n CatalogoProdutosAPI.Tests

3 - Projeto feito, agora é abrir o mesmo em File->Open Folder selecione CatalogoProdutosAPI.Tests, verá toda a estrutura deste projeto

4 - No terminal do VSCode digite C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI.Tests> cd ..

5 - No terminal do VSCode digite o seguinte comando C:\Users\seu-usuario\Documents\projects>dotnet add CatalogoProdutosAPI.Tests reference CatalogoProdutosAPI

6 - No arquivo CatalogoProdutosAPI.Tests.csproj do projeto CatalogoProdutosAPI.Tests verá uma linha de código adicional:

    <ItemGroup>
       <ProjectReference Include="..\CatalogoProdutosAPI\CatalogoProdutosAPI.csproj" />
    </ItemGroup>

7 - No terminal do VSCode digite o seguinte comando C:\Users\seu-usuario\Documents\projects>dotnet add CatalogoProdutosAPI.Tests package Moq

8 - No terminal do VSCode digite o seguinte comando C:\Users\seu-usuario\Documents\projects>dotnet add CatalogoProdutosAPI.Tests package Microsoft.AspNetCore.Mvc.Testing

9 - No terminal do VSCode digite C:\Users\seu-usuario\Documents\projects> cd CatalogoProdutosAPI.Tests

10 - No terminal do VSCode digite o seguinte comando C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI.Tests> dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 7.0.0

11 - No arquivo CatalogoProdutosAPI.Tests.csproj do projeto CatalogoProdutosAPI.Tests verá uma linha de código adicional:

     <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="7.0.0" />
     
12 - Agora é apenas testar o projeto CatalogoProdutosAPI.Tests, no terminal do VSCode digite C:\Users\seu-usuario\Documents\projects\CatalogoProdutosAPI.Tests> dotnet test

13 - Validando assim, alguns métodos do ProdutoRepository.cs do projeto principal CatalogoProdutosAPI     

===============================================================================================================

DOCUMENTAÇÃO RESUMIDA – ESTRUTURA DO PROJETO CatalogoProdutosAPI

&nbsp; ----------------

&nbsp; 1. VISÃO GERAL

&nbsp; ----------------

O projeto CatalogoProdutosAPI é uma API REST desenvolvida em ASP.NET

Core para gerenciamento de produtos, utilizando Entity Framework Core

com padrão Repository.

Estrutura principal:

CatalogoProdutosAPI │ ├── Controllers ├── Data ├── Dtos ├── Models ├──

Repositories

&nbsp; -----------

&nbsp; 2. MODELS

&nbsp; -----------

Produto.cs

Representa a entidade principal do sistema.



Propriedades: - Id → Identificador único - Nome → Nome do produto -

Categoria → Categoria do produto - Preco → Valor do produto - Status →

Ativo/Inativo - ImagemUrl → Caminho da imagem - DateCreated → Data de

criação

Função: Representa a tabela Produtos no banco de dados.

&nbsp; -----------------------------

&nbsp; 3. DATA (Contexto do Banco)

&nbsp; -----------------------------

IDataContext.cs

Define: - DbSet Produtos - Task SaveChangesAsync()

Permite desacoplamento e facilita testes.

DataContext.cs

Herda de DbContext e implementa IDataContext.

Responsável por: - Configuração do banco - Mapeamento das entidades -

Persistência de dados

&nbsp; ---------------------------------

&nbsp; 4. DTOS (Data Transfer Objects)

&nbsp; ---------------------------------

CriarProdutoDto.cs Utilizado no POST (criação). Contém: - Nome -

Categoria - Preço - Status - Imagem (IFormFile)

Permite upload de imagem via multipart/form-data.

EditarProdutoDto.cs Utilizado no PUT (edição). Contém: - Nome -

Categoria - Preço - Status - ImagemUrl

Evita expor diretamente o Model.

&nbsp; -----------------

&nbsp; 5. REPOSITORIES

&nbsp; -----------------

IProdutoRepository.cs

Define os métodos: - Cadastrar - Get - GetAll - GetFiltrado - Editar -

Deletar - SaveChangesAsync

ProdutoRepository.cs

Responsável por: - Inserção de produtos - Consulta por ID - Listagem

geral - Filtro avançado por: \* Categoria \* Preço mínimo \* Preço máximo \*

Status \* Presença de imagem - Atualização - Exclusão

Centraliza toda lógica de acesso ao banco.

&nbsp; ----------------

&nbsp; 6. CONTROLLERS

&nbsp; ----------------

ProdutoController.cs

Rota base: api/produto

Endpoints disponíveis:

POST /api/produto → Criar produto GET /api/produto → Listar todos GET

/api/produto/{id} → Buscar por ID GET /api/produto/filtro → Buscar com

filtros PUT /api/produto/{id} → Editar DELETE /api/produto/{id} →

Deletar

Upload de imagem: - Recebe IFormFile - Salva arquivo na pasta /Uploads -

Gera nome único com Guid - Armazena o caminho no banco

&nbsp; -----------------------

&nbsp; 7. FLUXO DA APLICAÇÃO

&nbsp; -----------------------

Cliente → Controller → Repository → DataContext → Banco de Dados

1\.  Cliente faz requisição HTTP

2\.  Controller recebe e valida

3\.  Repository executa operação

4\.  DataContext salva no banco

5\.  Resposta retorna ao cliente

&nbsp; -----------------------

&nbsp; 8. PADRÕES UTILIZADOS

&nbsp; -----------------------

\-   API REST

\-   Repository Pattern

\-   Injeção de Dependência

\-   Entity Framework Core

\-   DTO Pattern

\-   Upload de arquivos

&nbsp; -----------------

&nbsp; 9. RESUMO FINAL

&nbsp; -----------------

O projeto é uma API REST organizada e escalável que: - Gerencia

produtos - Permite upload de imagens - Suporta filtros dinâmicos -

Utiliza boas práticas de arquitetura

=========================================================================================================================

DOCUMENTAÇÃO RESUMIDA – PROJETO CatalogoProdutosAPI.Tests

&nbsp; ----------------

&nbsp; 1. VISÃO GERAL

&nbsp; ----------------

O projeto CatalogoProdutosAPI.Tests é responsável por testar

automaticamente as funcionalidades da camada de repositório da API.

Tecnologias utilizadas: - xUnit (framework de testes) - Entity Framework

Core InMemory - Testes assíncronos (async/await)

&nbsp; -------------------------

&nbsp; 2. ESTRUTURA DO PROJETO

&nbsp; -------------------------

CatalogoProdutosAPI.Tests │ └── ProdutoRepositoryTests.cs

&nbsp; ------------------------------

&nbsp; 3. ProdutoRepositoryTests.cs

&nbsp; ------------------------------

Classe principal: public class ProdutoRepositoryTests

Responsável por testar a classe ProdutoRepository.

&nbsp; -------------------------------------

&nbsp; 4. CONFIGURAÇÃO DO BANCO EM MEMÓRIA

&nbsp; -------------------------------------

Método auxiliar:

private DataContext CriarContextoInMemory()

Função: - Configura DbContext com UseInMemoryDatabase(“BancoTeste”) -

Cria um banco temporário apenas em memória - Retorna uma instância de

DataContext

Isso permite que os testes rodem sem depender de um banco real.

&nbsp; -----------------------

&nbsp; 5. TESTE IMPLEMENTADO

&nbsp; -----------------------

Teste: Deve\_Cadastrar\_Produto()

Estrutura utilizada: AAA (Arrange, Act, Assert)

1\)  Arrange (Preparação)

\-   Cria o contexto em memória

\-   Instancia o repositório

\-   Cria um objeto Produto

2\)  Act (Execução)

\-   Executa o método Cadastrar

\-   Busca todos os produtos

3\)  Assert (Validação)

\-   Verifica se existe exatamente 1 produto cadastrado usando

&nbsp;   Assert.Single(produtos)

&nbsp; -------------------

&nbsp; 6. FLUXO DO TESTE

&nbsp; -------------------

Criar contexto em memória ↓ Criar repositório ↓ Cadastrar produto ↓

Buscar produtos ↓ Validar resultado

&nbsp; ----------------------------------

&nbsp; 7. OBJETIVO DO PROJETO DE TESTES

&nbsp; ----------------------------------

\-   Garantir que a camada Repository funciona corretamente

\-   Evitar regressões no código

\-   Validar persistência de dados

\-   Testar sem depender de banco real

\-   Permitir integração contínua (CI/CD)

&nbsp; ---------------

&nbsp; 8. BENEFÍCIOS

&nbsp; ---------------

\-   Testes rápidos

\-   Ambiente isolado

\-   Fácil manutenção

\-   Segurança ao refatorar código

\-   Melhor qualidade da aplicação

&nbsp; --------------

&nbsp; RESUMO FINAL

&nbsp; --------------

O projeto CatalogoProdutosAPI.Tests valida o funcionamento da camada de

acesso a dados da API utilizando testes automatizados com xUnit e banco

de dados em memória, garantindo maior confiabilidade e qualidade ao

sistema.
