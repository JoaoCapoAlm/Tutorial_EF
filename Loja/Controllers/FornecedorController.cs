using Loja.Data;
using Loja.Data.Dtos;
using Loja.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Loja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly LojaDbContext _context;
        public FornecedorController(LojaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Fornecedor), 201)]
        public async Task<IActionResult> Create([FromBody] FornecedorDto fornecedorDto)
        {
            var pdt = await _context.Fornecedor.AddAsync(new Fornecedor()
            {
                Nome = fornecedorDto.Nome,
                Cnpj = fornecedorDto.Cnpj,
                Email = fornecedorDto.Email,
                Endereco = fornecedorDto.Endereco,
                Telefone = fornecedorDto.Telefone
            });

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFornecedor), new { pdt.Entity.Id }, pdt.Entity);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Fornecedor), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFornecedor([FromRoute] int Id)
        {
            var forncedor = await _context.Fornecedor
                .AsNoTracking()
                .Where(x => x.Id.Equals(Id))
                .Include(x => x.Produtos)
                .FirstOrDefaultAsync();

            if (forncedor == null)
                return NotFound("Fornecedor não encontrado");

            return Ok(forncedor);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<Fornecedor>), 200)]
        public async Task<IActionResult> List()
        {
            var list = await _context.Fornecedor.AsNoTracking().Include(x => x.Produtos).ToListAsync();

            return Ok(list);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(Fornecedor), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] FornecedorDto dto)
        {
            var fornecedor = await _context.Fornecedor.FirstAsync(x => x.Id.Equals(Id));
            if (fornecedor == null)
                return NotFound("Fornecedor não encontrado");

            fornecedor.Nome = dto.Nome;
            fornecedor.Cnpj = dto.Cnpj;
            fornecedor.Email = dto.Email;
            fornecedor.Endereco = dto.Endereco;
            fornecedor.Telefone = dto.Telefone;

            await _context.SaveChangesAsync();

            return Ok(fornecedor);
        }
    }
}
