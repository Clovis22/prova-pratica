using Xunit;
using Microsoft.EntityFrameworkCore;
using CatalogoProdutosAPI.Data;
using CatalogoProdutosAPI.Models;
using CatalogoProdutosAPI.Repositories;
using System.Threading.Tasks;

namespace CatalogoProdutosAPI.Tests
{
    public class ProdutoRepositoryTests
    {
         private DataContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "BancoTeste")
                .Options;

            return new DataContext(options);
        }

        [Fact]
        public async Task Deve_Cadastrar_Produto()
        {
            // Arrange
            var contexto = CriarContextoInMemory();
            var repository = new ProdutoRepository(contexto);

            var produto = new Produto
            {
                Nome = "Produto Teste",
                Categoria = "Eletronico",
                Preco = 100,
                Status = true
            };

            // Act
            await repository.Cadastrar(produto);
            var produtos = await repository.GetAll();

            // Assert
            Assert.Single(produtos);
        }
    }
}