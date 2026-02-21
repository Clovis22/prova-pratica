using Microsoft.EntityFrameworkCore;
using CatalogoProdutosAPI.Models;

namespace CatalogoProdutosAPI.Data
{
    public interface IDataContext
    {
         
        DbSet<Produto> Produtos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken tokenDeCancelamento = default);

    }
}