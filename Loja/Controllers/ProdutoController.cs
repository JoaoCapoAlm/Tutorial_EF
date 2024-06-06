using Loja.Data;
using Loja.Data.Dtos;
using Loja.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Loja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly LojaDbContext _context;
        public ProdutoController(LojaDbContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Create([FromBody] ProdutoDto produtoDto)
        {
            var pdt = await _context.Produto.AddAsync(new Produto()
            {
                Nome = produtoDto.Nome,
                FornecedorId = produtoDto.FornecedorId,
                Preco = produtoDto.Preco
            });
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduto), new { pdt.Entity.Id }, pdt.Entity);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Produto>> GetProduto([FromRoute] int Id)
        {
            var produto = await _context.Produto
                .AsNoTracking()
                .Where(x => x.Id.Equals(Id))
                .Include(x => x.Fornecedor)
                .FirstOrDefaultAsync();

            if (produto == null)
                return NotFound("Produto não encontrado");

            return Ok(produto);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var produtos = await _context.Produto.AsNoTracking().Include(x => x.Fornecedor).ToListAsync();

            return Ok(produtos);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] ProdutoDto dto)
        {
            var produto = await _context.Produto.FirstAsync(x => x.Id.Equals(Id));
            if(produto == null)
                return NotFound("Produto não encontrado");

            produto.Nome = dto.Nome;
            produto.Preco = dto.Preco;
            produto.FornecedorId = dto.FornecedorId;

            await _context.SaveChangesAsync();

            return Ok(produto);
        }
    }
}
