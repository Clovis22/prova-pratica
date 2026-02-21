using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogoProdutosAPI.Models;

namespace CatalogoProdutosAPI.Repositories
{
    public interface IProdutoRepository
    {

        Task Cadastrar(Produto produto);

        Task<Produto> Get(int id);

        Task<IEnumerable<Produto>> GetAll();

        Task<IEnumerable<Produto>> GetFiltrado(string? categoria, decimal? precoMin, decimal? precoMax, bool? status, bool? temImagem);

        Task Deletar(int id);

        Task Editar(Produto produto);

        Task<bool> SaveChangesAsync();

    }
}