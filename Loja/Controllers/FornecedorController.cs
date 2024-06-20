using Loja.Data.Dtos;
using Loja.Models;
using Loja.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class FornecedorController : ControllerBase
    {
        private readonly FornecedorService _service;
        public FornecedorController(FornecedorService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Fornecedor), 201)]
        public async Task<IActionResult> Create([FromBody] FornecedorDto fornecedorDto)
        {
            var fornecedor = await _service.AddFornecedorAsync(fornecedorDto);
            return CreatedAtAction(nameof(GetFornecedor), new { fornecedor.Id }, fornecedor);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Fornecedor), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFornecedor([FromRoute] int Id)
        {
            var forncedor = await _service.GetFornecedorByIdAsync(Id);

            if (forncedor == null)
                return NotFound("Fornecedor não encontrado");

            return Ok(forncedor);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<Fornecedor>), 200)]
        public async Task<IActionResult> List()
        {
            var list = await _service.GetAllFornecedorAsync();

            return Ok(list);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(Fornecedor), 204)]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] FornecedorDto dto)
        {
            await _service.UpdateFornecedorAsync(Id, dto);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            await _service.DeleteFornecedorAsync(Id);

            return NoContent();
        }
    }
}
