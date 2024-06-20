using Loja.Data;
using Loja.Data.Dtos;
using Loja.Models;
using Microsoft.EntityFrameworkCore;

namespace Loja.Services
{
    public class ProdutoService
    {
        private readonly LojaDbContext _context;
        public ProdutoService(LojaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllProductsAsync()
        {
            return await _context.Produto.ToArrayAsync();
        }

        public async Task<Produto?> GetProductByIdAsync(int id)
        {
            return await _context.Produto.FindAsync(id);
        }

        public async Task<Produto> AddProductAsync(ProdutoDto dto)
        {
            var produto = await _context.Produto.AddAsync(new Produto()
            {
                FornecedorId = dto.FornecedorId,
                Nome = dto.Nome,
                Preco = dto.Preco
            });
            await _context.SaveChangesAsync();

            return produto.Entity;
        }

        public async Task UpdateProductAsync(int id, ProdutoDto dto)
        {

            var produto = await _context.Produto.FirstAsync(x => x.Id.Equals(id));
            if (produto == null)
                throw new KeyNotFoundException();

            produto.Nome = dto.Nome;
            produto.Preco = dto.Preco;
            produto.FornecedorId = dto.FornecedorId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
