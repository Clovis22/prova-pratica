# prova-pratica

Desafio: Cadastro e Consulta de Produtos

Instruções para o PROJETO CatalogoProdutosAPI

1 - Rode a instrução "docker compose up --build" no terminal do VSCode, no DockerDesktop estará assim:
    postgres
    dpage/pgadmin4
    catalogoprodutosapi-api

2 - No browser digite a seguinte Url http://localhost:5000/swagger. A comunicação é tudo feito pelo docker.

OBSERVAÇÃO: Se no DockerDesktop estiver apenas as imagens: postgres, dpage/pgadmin4. Apenas rode novamente a instrução "docker compose up --build" no terminal do VSCode, pois pode ocorrer timeout, ou seja, a rede pode está lenta ou download travando das imagens.

API dockerizado
PostgreSQL dockerizado
pgAdimin dockerizado
Migrations automáticas
Banco feito automáticamente
API apenas inicia quando o banco estiver feito

=====================================================================================

Instruções para o PROJETO CatalogoProdutosAPI.Tests

1 - Abra o PROJETO CatalogoProdutosAPI.Tests no VSCode e no terminal rode a instrução "dotnet test", para validar os testes

===============================================================================================================

DOCUMENTAÇÃO RESUMIDA – ESTRUTURA DO PROJETO CatalogoProdutosAPI

&nbsp; ----------------

&nbsp; 1. VISÃO GERAL

&nbsp; ----------------

O projeto CatalogoProdutosAPI é uma API REST desenvolvida em ASP.NET

Core para gerenciamento de produtos, utilizando Entity Framework Core

com padrão Repository.

Estrutura principal:

CatalogoProdutosAPI │ Controllers ── Data ── Dtos ── Models ──

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

==============================================================================================

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

CatalogoProdutosAPI.Tests │ ── ProdutoRepositoryTests.cs

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
