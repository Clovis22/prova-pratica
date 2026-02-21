using Microsoft.AspNetCore.Http;

namespace CatalogoProdutosAPI.Dtos
{
    public class CriarProdutoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public bool Status { get; set; }
        public IFormFile? Imagem { get; set; }
    }
}