using Loja.Data.Dtos;
using Loja.Data.ViewModels;
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
    public class VendaController : ControllerBase
    {
        private readonly VendaService _service;
        public VendaController(VendaService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Fornecedor), 201)]
        public async Task<IActionResult> Create([FromBody] VendaDto dto)
        {
            var venda = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetVenda), new { venda.Id }, venda);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<Venda>), 200)]
        public async Task<IActionResult> List()
        {
            var list = await _service.GetAllAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Venda), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetVenda([FromRoute] int id)
        {
            var venda = await _service.GetVendaAsync(id);
            if (venda == null)
                return NotFound("Venda não encontrada");

            return Ok(venda);
        }

        [HttpGet("cliente/{clienteId}/detalhado")]
        [ProducesResponseType(typeof(IEnumerable<VendaClienteDetalhadaVM>), 200)]
        public async Task<IActionResult> GetVendasDetalhadasByClienteIdAsync([FromRoute] int id)
        {
            var venda = await _service.GetVendasDetalhadasByClienteIdAsync(id);

            return Ok(venda);
        }

        [HttpGet("cliente/{clienteId}/sumarizado")]
        [ProducesResponseType(typeof(VendaClienteSumarizadoVM), 200)]
        public async Task<IActionResult> GetVendasSumarizadasByClienteIdAsync([FromRoute] int id)
        {
            var venda = await _service.GetVendasSumarizadasByClienteIdAsync(id);

            return Ok(venda);
        }
    }
}
