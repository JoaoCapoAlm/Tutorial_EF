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
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;
        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ClienteVM>), 200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<IActionResult> List()
        {
            var list = await _service.GetAllClientAsync();
            
            return Ok(list);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteVM), 201)]
        public async Task<IActionResult> Create([FromBody] ClienteDto dto)
        {
            var cliente = await _service.AddClientAsync(dto);
            return CreatedAtAction(nameof(GetCliente), new { cliente.Id }, cliente);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> GetCliente([FromRoute] int Id)
        {
            var cliente = await _service.GetClientByIdAsync(Id);

            if (cliente == null)
                return NotFound("Cliente não encontrado");

            return Ok(cliente);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] ClienteDto dto)
        {
            await _service.UpdateClienteAsync(Id, dto);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var token = await _service.LoginAsync(email, password);
            return Ok(token);
        }
    }
}
