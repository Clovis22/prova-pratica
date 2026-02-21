using System;

namespace CatalogoProdutosAPI.Models
{
    public class Produto
    {

        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public bool Status { get; set; }
        public string? ImagemUrl { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        
    }
}