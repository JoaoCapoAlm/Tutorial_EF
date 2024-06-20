using Loja.Data;
using Loja.Data.Dtos;
using Loja.Data.ViewModels;
using Loja.Models;
using Microsoft.EntityFrameworkCore;

namespace Loja.Services
{
    public class VendaService
    {
        private readonly LojaDbContext _context;
        public VendaService(LojaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venda>> GetAllAsync()
        {
            return await _context.Venda.AsNoTracking().ToArrayAsync();
        }

        public async Task<Venda?> GetVendaAsync(int id)
        {
            return await _context.Venda.AsNoTracking().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Venda> AddAsync(VendaDto dto)
        {
            var venda = await _context.Venda.AddAsync(new Venda()
            {
                ClienteId = dto.ClienteId,
                DataHora = dto.DataHora,
                NotaFiscal = dto.NotaFiscal,
                PrecoUnitario = dto.PrecoUnitario,
                ProdutoId = dto.ProdutoId,
                QtdProduto = dto.QtdProduto
            });
            await _context.SaveChangesAsync();

            return venda.Entity;
        }

        public async Task<IEnumerable<VendaClienteDetalhadaVM>> GetVendasDetalhadasByClienteIdAsync(int clienteId) {
            return await _context.Venda.AsNoTracking()
                .Where(x => x.ClienteId.Equals(clienteId))
                .Select(x => new VendaClienteDetalhadaVM(
                    x.Id,
                    x.ClienteId,
                    x.Cliente.Nome,
                    x.DataHora,
                    x.Produto.Nome,
                    x.QtdProduto,
                    x.PrecoUnitario
                ))
                .ToArrayAsync();
        }

        public async Task<VendaClienteSumarizadoVM> GetVendasSumarizadasByClienteIdAsync(int clienteId)
        {
            var nomeCliente = await _context.Cliente
                .AsNoTracking()
                .Where(x => x.Id.Equals(clienteId))
                .Select(x => x.Nome)
                .FirstOrDefaultAsync();

            var vendas = await _context.Venda
                .AsNoTracking()
                .Where(x => x.ClienteId.Equals(clienteId))
                .Select(x => new
                {
                    IdProduto = x.ProdutoId,
                    TotalVenda = x.PrecoUnitario * x.QtdProduto,
                    QtdVendida = x.QtdProduto
                })
                .ToArrayAsync();

            var idProdutos = vendas.Select(x => x.IdProduto).Distinct().ToArray();

            var produtos = await _context.Produto
                .AsNoTracking()
                .Where(x => idProdutos.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, x => x.Nome);

            int totalVendas = vendas.Length;
            double somaTotalCobrado = vendas.Select(x => x.TotalVenda).ToArray().Sum();

            var produtosSumarizados = vendas
                .GroupBy(x => x.IdProduto)
                .Select(x => new VendaProdutoSumarizadoVM(
                    x.Key,
                    produtos[x.Key],
                    x.Count(),
                    x.Select(y => y.QtdVendida).ToArray().Sum(),
                    x.Select(y => y.TotalVenda).ToArray().Sum()
                ))
                .ToArray();


            return new VendaClienteSumarizadoVM(clienteId, nomeCliente, totalVendas, produtosSumarizados, somaTotalCobrado);
        }
    }
}
