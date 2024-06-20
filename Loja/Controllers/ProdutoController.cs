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
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;
        public ProdutoController(ProdutoService service) {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Create([FromBody] ProdutoDto produtoDto)
        {

            var produto = await _service.AddProductAsync(produtoDto);
            return CreatedAtAction(nameof(GetProduto), new { produto.Id }, produto);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Produto), 200)]
        public async Task<IActionResult> GetProduto([FromRoute] int Id)
        {
            var produto = await _service.GetProductByIdAsync(Id);
            if(produto == null)
                return NotFound("Produto não encontrado");

            return Ok(produto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Produto>), 200)]
        public async Task<IActionResult> List()
        {
            var produtos = await _service.GetAllProductsAsync();

            return Ok(produtos);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] ProdutoDto dto)
        {
            try
            {
                await _service.UpdateProductAsync(Id, dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Produto não encontrado");
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            await _service.DeleteProductAsync(Id);
            return NoContent();
        }
    }
}
