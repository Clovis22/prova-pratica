# prova-pratica

Prova pratica gerenciamento de catalogo de produtos



Instruções para o PROJETO CatalogoProdutosAPI



1 - Instalação do Docker Desktop Installer

2 - Instalação do dotnet-sdk-6.0.428-win-x64

3 - no cmd na raiz do seu usuario instale dotnet tool install --global dotnet-ef --version 6.\*

4 - no VSCode instalei as extensões C#, C# Dev Kit, IntelliCode For C# Dev Kit e C# Extentions autor jchannon

5 - comando dotnet run para roda a aplicação no browser, http://localhost:5289/swagger/index.html

6 - Como subir o arquivo docker-compose.yml, comando no terminal do vscode docker-compose up -d, isso ler o docker-compose cria os conteiners e grava variaveis do .env

7 - Utilizando docker-compose down para fazer parar os conteiners, terminal vscode

8 - Integração com o banco, comando dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 5.0.2, terminal vscode

9 - Permitir usar migração, comando dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.428, terminal vscode

10 - Instalar o EntityFramework,comando dotnet add package Microsoft.EntityFrameworkCore --version 6.0.0, terminal vscode

11 - Forma de corrigir versão, se aparecer erro:

&nbsp;    dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0

&nbsp;    dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 6.0.0

&nbsp;    dotnet add package Microsoft.EntityFrameworkCore --version 6.0.0

12 - E o arquivo .env.guia, depois é somente renomear para .env e inserir as configurações de conexão, onde houver "configurar_aqui"

     POSTGRES_USER=configurar_aqui
     POSTGRES_PASSWORD=configurar_aqui
     POSTGRES_DB=configurar_aqui

     PGADMIN_DEFAULT_EMAIL=pgadmin4@pgadmin.org
     PGADMIN_DEFAULT_PASSWORD=configurar_aqui

     ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=configurar_aqui;Username=configurar_aqui;Password=configurar_aqui



===============================================================================================================



Instruções para o PROJETO CatalogoProdutosAPI.Tests



Observação: Estrutura C:\\Users\\clovis\\Documents\\projects

dentro da pasta projects tem o projeto principal CatalogoProdutosAPI



1 - Para criar o projeto de testes, abra o prompt dentro da pasta projects com o comando dotnet new xunit -n CatalogoProdutosAPI.Tests

&nbsp;   - Ai terá os dois projetos a API CatalogoProdutosAPI e Testes CatalogoProdutosAPI.Tests

2 - Rode o comando dotnet add CatalogoProdutosAPI.Tests reference CatalogoProdutosAPI dentro da pasta projects

3 - Rode mais esses comandos dentro de projects:

&nbsp;   - dotnet add CatalogoProdutosAPI.Tests package Moq

&nbsp;   - dotnet add CatalogoProdutosAPI.Tests package Microsoft.AspNetCore.Mvc.Testing

&nbsp;   - dotnet add CatalogoProdutosAPI.Tests package Microsoft.EntityFrameworkCore.InMemory

4 - Entre na pasta do projeto de testes:

&nbsp;   cd CatalogoProdutosAPI.Testes

&nbsp;   abra o prompt e rode o comando dotnet add reference ../CatalogoProdutosAPI/CatalogoProdutosAPI.csproj

5 - No arquivo CatalogoProdutosAPI.Tests.csproj terá <ProjectReference Include="..\\CatalogoProdutosAPI\\CatalogoProdutosAPI.csproj" />

6 - Comando para rodar o teste dotnet test no terminal vscode do projeto CatalogoProdutosAPI.Tests

7 - Agora para instalar outro pacote com versão especifica entre na pasta CatalogoProdutosAPI.Tests abra o prompt e rode o comando dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 7.0.0, rodei o teste com essa verão

8 - Aparecera a linha de código <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="7.0.0" /> no arquivo CatalogoProdutosAPI.Tests.csproj

9 - Rode o comando dotnet test no terminal vscode novamente para validar o teste



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

