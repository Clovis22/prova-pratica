using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogoProdutosAPI.Models;
using CatalogoProdutosAPI.Data;
using CatalogoProdutosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoProdutosAPI.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly IDataContext _contexto;

        public ProdutoRepository(IDataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task Cadastrar(Produto produto)
        {
            await _contexto.Produtos.AddAsync(produto);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Produto> Get(int id)
        {
            Produto? produto = await _contexto.Produtos.FindAsync(id);
            if (produto == null)
                throw new NullReferenceException("Produto não encontrado");
            return produto;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _contexto.Produtos.ToListAsync();
        }
        
        public async Task<IEnumerable<Produto>> GetFiltrado(string? categoria, decimal? precoMin, decimal? precoMax, bool? status, bool? temImagem)
        {
            var query = _contexto.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(p => p.Categoria == categoria);

            if (precoMin.HasValue)
                query = query.Where(p => p.Preco >= precoMin.Value);

            if (precoMax.HasValue)
                query = query.Where(p => p.Preco <= precoMax.Value);

            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

            if (temImagem.HasValue)
            {
                if (temImagem.Value)
                    query = query.Where(p => !string.IsNullOrEmpty(p.ImagemUrl));
                else
                    query = query.Where(p => string.IsNullOrEmpty(p.ImagemUrl));
            }

            return await query.ToListAsync();
        }

        public async Task Deletar(int id)
        {
            var itemParaDeletar = await _contexto.Produtos.FindAsync(id);
            if(itemParaDeletar == null)
               throw new NullReferenceException("Produto Não Encontrado!");

            _contexto.Produtos.Remove(itemParaDeletar);
            await _contexto.SaveChangesAsync();
        }

        public async Task Editar(Produto produto)
        {
            var itemParaEditar = await _contexto.Produtos.FindAsync(produto.Id);
            if(itemParaEditar == null)
               throw new NullReferenceException("Produto Não Encontrador");
            
            itemParaEditar.Nome = produto.Nome;
            itemParaEditar.Preco = produto.Preco;
            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _contexto.SaveChangesAsync() > 0;
        }
        
    }
}