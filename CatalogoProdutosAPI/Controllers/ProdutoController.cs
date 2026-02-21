using System.Threading.Tasks;
using CatalogoProdutosAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using CatalogoProdutosAPI.Models;
using CatalogoProdutosAPI.Repositories;

namespace CatalogoProdutosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        // GET api/produto
        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<ActionResult> CriarProduto([FromForm] CriarProdutoDto criarProdutoDto)
        {
            string? imagemUrl = null;

            if (criarProdutoDto.Imagem != null && criarProdutoDto.Imagem.Length > 0)
            {
                // Define pasta onde vai salvar a imagem
                var pasta = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                // Cria nome único
                var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(criarProdutoDto.Imagem.FileName)}";
                var caminhoCompleto = Path.Combine(pasta, nomeArquivo);

                // Salva no disco
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await criarProdutoDto.Imagem.CopyToAsync(stream);
                }

                // Salva o caminho ou URL relativo
                imagemUrl = $"/Uploads/{nomeArquivo}";
            }

            Produto produto = new()
            {
                Nome = criarProdutoDto.Nome,
                Categoria = criarProdutoDto.Categoria,
                Preco = criarProdutoDto.Preco,
                Status = criarProdutoDto.Status,
                ImagemUrl = imagemUrl,
                DateCreated = DateTime.UtcNow
            };

            await _produtoRepository.Cadastrar(produto);
            return Ok(produto);
        }

        // GET api/produto/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _produtoRepository.Get(id);
            if(produto == null)
               return NotFound();

            return Ok(produto); 
        }

        // GET api/produto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetAll();
            return Ok(produtos); 
        }

        // GET api/produto/filtro
        [HttpGet("filtro")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetFiltrado([FromQuery] string? categoria, [FromQuery] decimal? precoMin, [FromQuery] decimal? precoMax, [FromQuery] bool? status, [FromQuery] bool? temImagem)
        {
            var produtos = await _produtoRepository.GetFiltrado(categoria, precoMin, precoMax, status, temImagem);
            return Ok(produtos);
        }

        // DELETE api/produto/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarProduto(int id)
        {
            await _produtoRepository.Deletar(id);
            return Ok();
        }

        // PUT api/produto/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> EditarProduto(int id, EditarProdutoDto editarProdutoDto)
        {
            var produto = await _produtoRepository.Get(id);
            if (produto == null)
                return NotFound();

            produto.Nome = editarProdutoDto.Nome;
            produto.Categoria = editarProdutoDto.Categoria;
            produto.Preco = editarProdutoDto.Preco;
            produto.Status = editarProdutoDto.Status;
            produto.ImagemUrl = editarProdutoDto.ImagemUrl;
            
            await _produtoRepository.Editar(produto);
            return Ok();
        }

    }
}