using Microsoft.EntityFrameworkCore;
using CatalogoProdutosAPI.Models;

namespace CatalogoProdutosAPI.Data
{
    public class DataContext: DbContext, IDataContext
    {
        
        public DataContext(DbContextOptions<DataContext> opcoes): base(opcoes) 
        {

        }

        public DbSet<Produto> Produtos { get; set; }

    }
}