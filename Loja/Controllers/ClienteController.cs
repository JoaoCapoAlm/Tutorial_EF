using Loja.Data;
using Loja.Data.Dtos;
using Loja.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Loja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly LojaDbContext _context;
        public ClienteController(LojaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<Cliente>), 200)]
        public async Task<IActionResult> List()
        {
            var list = await _context.Cliente.AsNoTracking().ToListAsync();
            
            return Ok(list);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Cliente), 201)]
        public async Task<IActionResult> Create([FromBody] ClienteDto dto)
        {
            var cliente = await _context.Cliente.AddAsync(new Cliente()
            {
                Nome = dto.Nome,
                Cpf = dto.Cpf,
                Email = dto.Email
            });
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new { cliente.Entity.Id }, cliente.Entity);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCliente([FromRoute] int Id)
        {
            var cliente = await _context.Cliente
                .AsNoTracking()
                .Where(x => x.Id.Equals(Id))
                .FirstOrDefaultAsync();

            if (cliente == null)
                return NotFound("Cliente não encontrado");

            return Ok(cliente);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] ClienteDto dto)
        {
            var cliente = await _context.Cliente.FirstAsync(x => x.Id.Equals(Id));
            if (cliente == null)
                return NotFound("Cliente não encontrado");

            cliente.Nome = dto.Nome;
            cliente.Cpf = dto.Cpf;
            cliente.Email = dto.Email;

            await _context.SaveChangesAsync();
            return Ok(cliente);
        }
    }
}
