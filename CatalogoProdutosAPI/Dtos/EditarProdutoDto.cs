namespace CatalogoProdutosAPI.Dtos
{
    public class EditarProdutoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public bool Status { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;
    }
}