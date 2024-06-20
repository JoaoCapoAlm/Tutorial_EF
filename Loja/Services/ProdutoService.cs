using Loja.Data;
using Loja.Data.Dtos;
using Loja.Data.ViewModels;
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

        public async Task<IEnumerable<VendaProdutoDetalhadaVM>> GetVendasDetalhadasByProdutoId(int id)
        {
            return await _context.Venda
                .AsNoTracking()
                .Where(x => x.ProdutoId.Equals(id))
                .Include(x => x.Produto)
                .Include(x => x.Cliente)
                .Select(x => new VendaProdutoDetalhadaVM(
                    x.Id,
                    x.ProdutoId,
                    x.Produto.Nome,
                    x.DataHora,
                    x.Cliente.Nome,
                    x.QtdProduto,
                    x.PrecoUnitario * x.QtdProduto
                ))
                .ToArrayAsync();
        }

        public async Task<VendaProdutoSumarizadoVM> GetVendasSumerizadasByProdutoId(int id)
        {
            var nomeProduto = await _context.Produto.AsNoTracking().Where(x => x.Id.Equals(id)).Select(x => x.Nome).FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(nomeProduto))
                throw new KeyNotFoundException();

            var vendas = await _context.Venda
                .AsNoTracking()
                .Where(x => x.ProdutoId.Equals(id))
                .Select(x => new
                {
                    TotalVenda = x.PrecoUnitario * x.QtdProduto,
                    QtdVendida = x.QtdProduto
                })
                .ToArrayAsync();

            int totalVendas = vendas.Length;
            int totalUnidadesVendidas = vendas.Select(x => x.QtdVendida).ToArray().Sum();
            double somaTotalCobrado = vendas.Select(x => x.TotalVenda).ToArray().Sum();

            return new VendaProdutoSumarizadoVM(id, nomeProduto, totalVendas, totalUnidadesVendidas, somaTotalCobrado);
        }
    }
}
